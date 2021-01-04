using Armazenagem3L_API.ExceptionHandler;
using Armazenagem3L_API.Logger;
using Armazenagem3L_API.Models;
using Armazenagem3L_API.Repositories;
using Armazenagem3L_API.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Transactions;

namespace Armazenagem3L_API.Services {
    public class CargaService {
        private readonly ICargaRepository _repository;
        private readonly ILoggerManager _logger;
        private readonly IProdutosRepository _produto;
        private readonly IMotoristaRepository _motorista;

        public CargaService(ICargaRepository carga, IProdutosRepository produto, IMotoristaRepository motorista, ILoggerManager logger) {
            _repository = carga;
            _produto = produto;
            _motorista = motorista;
            _logger = logger;
        }

        public CustomResponse listagemCargas() {
            _logger.LogDebug("[INFO] Executando funcao (Service): listagemProdutos");

            try {
                Carga[] result = _repository.GetCargas();

                if (result.Length > 0) {
                    foreach (var item in result) {
                        BuscaProdutos(item);
                    }
                }

                return new CustomResponse(HttpStatusCode.OK, null, result);
            } catch (ApiCustomException ex) {
                CustomHandler RecuperaExcecao = JsonSerializer.Deserialize<CustomHandler>(ex.Message);
                return new CustomResponse(RecuperaExcecao.StatusCode, new CustomMessage(RecuperaExcecao.Nome, RecuperaExcecao.Descricao), null);
            }

        }

        public CustomResponse cargaByIdAndMotoristaId(int cargaId, int motoristaId) {
            _logger.LogDebug("[INFO] Executando funcao (Service): cargaByIdAndMotoristaId  => carga " + JsonSerializer.Serialize(cargaId) + " motorista " + JsonSerializer.Serialize(motoristaId));

            try {
                var carga = _repository.cargaByIdAndMotoristaId(cargaId, motoristaId);

                if (carga == null) {
                    CustomHandler h = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.CARGA_NAO_ENCONTRADA);
                    throw new ApiCustomException(JsonSerializer.Serialize(h));
                } else {
                    BuscaProdutos(carga);
                }

                return new CustomResponse(HttpStatusCode.OK, null, carga);
            } catch (ApiCustomException ex) {
                CustomHandler RecuperaExcecao = JsonSerializer.Deserialize<CustomHandler>(ex.Message);
                return new CustomResponse(RecuperaExcecao.StatusCode, new CustomMessage(RecuperaExcecao.Nome, RecuperaExcecao.Descricao), null);
            }


        }

        public CustomResponse cargaByMotoristaId(int id)
        {
            _logger.LogDebug("[INFO] Executando funcao (Service): ProdutosById  Produto =>" + JsonSerializer.Serialize(id));

            try {
                Carga[] result = _repository.cargaByMotoristaId(id);

                if (result.Length > 0) {
                    foreach (var item in result) {
                        BuscaProdutos(item);
                    }
                } else {
                    CustomHandler h = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.CARGA_NAO_ENCONTRADA);
                    throw new ApiCustomException(JsonSerializer.Serialize(h));
                }

                return new CustomResponse(HttpStatusCode.OK, null, result);
            } catch (ApiCustomException ex) {
                CustomHandler RecuperaExcecao = JsonSerializer.Deserialize<CustomHandler>(ex.Message);
                return new CustomResponse(RecuperaExcecao.StatusCode, new CustomMessage(RecuperaExcecao.Nome, RecuperaExcecao.Descricao), null);
            }
        }

        public CustomResponse cargaById(int id)
        {
            _logger.LogDebug("[INFO] Executando funcao (Service): ProdutosById  Produto =>" + JsonSerializer.Serialize(id));

            try {
                Carga carga = _repository.GetCargaById(id);

                if (carga == null) {
                    CustomHandler h = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.CARGA_NAO_ENCONTRADA);
                    throw new ApiCustomException(JsonSerializer.Serialize(h));
                } else {
                    BuscaProdutos(carga);
                }
                return new CustomResponse(HttpStatusCode.OK, null, carga);
            } catch (ApiCustomException ex) {
                CustomHandler RecuperaExcecao = JsonSerializer.Deserialize<CustomHandler>(ex.Message);
                return new CustomResponse(RecuperaExcecao.StatusCode, new CustomMessage(RecuperaExcecao.Nome, RecuperaExcecao.Descricao), null);
            }
        }

        public CustomResponse Add(Carga carga) {
            _logger.LogDebug("[INFO] Executando funcao (Service): Add Carga =>" + JsonSerializer.Serialize(carga));
            ArrayList ProdutosAlterados = new ArrayList();
            try {
                using (var transaction = new TransactionScope()) {
                    _repository.Add(carga);

                    IEnumerable<ProdutoQtd> Ids = carga.Produtos;

                    foreach (var item in Ids) {
                        Produto p = _produto.GetProdutoById(item.ProdutoId);
                        if (p == null) {
                            CustomHandler h = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.ERRO_BUSCA_PRODUTO);
                            throw new ApiCustomException(JsonSerializer.Serialize(h));
                        } else {
                            if (item.Qtd > p.Qtd) {
                                CustomHandler h = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.ERRO_SALVAR_CARGA);
                                throw new ApiCustomException(JsonSerializer.Serialize(h));
                            } else {
                                p.Qtd -= item.Qtd;
                                ProdutosAlterados.Add(p);
                            }
                        }

                    }

                    if (_repository.SaveChanges()) {
                        AtualizaProdutos(ProdutosAlterados);
                        InsereCargaProdutos(carga.Produtos, _repository.GetLast().Id);
                        transaction.Complete();
                    } else {
                        transaction.Dispose();
                        CustomHandler h = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.CARGA_ADD_FALHA);
                        throw new ApiCustomException(JsonSerializer.Serialize(h));
                    }

                }

                return new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.CARGA_ADD_SUCESSO), null);
            } catch (ApiCustomException ex) {
                _logger.LogDebug("[ERRO] Ocorrencia de erro (Service): Add Carga =>" + JsonSerializer.Serialize(ex.InnerException));
                CustomHandler RecuperaExcecao = JsonSerializer.Deserialize<CustomHandler>(ex.Message);
                return new CustomResponse(RecuperaExcecao.StatusCode, new CustomMessage(RecuperaExcecao.Nome, RecuperaExcecao.Descricao), null);
            }
        }

        public CustomResponse AceitarCarga(MotoristaCarga carga) {
            _logger.LogDebug("[INFO] Executando funcao (Service): AceitarCarga Carga =>" + JsonSerializer.Serialize(carga));

            try {
                var CargaEscolhida = _repository.FindById(carga.CargaId);

                CargaEscolhida.MotoristaId = carga.MotoristaId;
                _repository.Update(CargaEscolhida);
                if (!_repository.SaveChanges()) {
                    CustomHandler h = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.CARGA_ACEITA_ERRO);
                    throw new ApiCustomException(JsonSerializer.Serialize(h));
                }
                return new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.CARGA_ACEITA), null);
            } catch (ApiCustomException ex) {
                _logger.LogDebug("[ERRO] Ocorrencia de erro (Service): AceitarCarga Carga =>" + JsonSerializer.Serialize(ex.InnerException));
                CustomHandler RecuperaExcecao = JsonSerializer.Deserialize<CustomHandler>(ex.Message);
                return new CustomResponse(RecuperaExcecao.StatusCode, new CustomMessage(RecuperaExcecao.Nome, RecuperaExcecao.Descricao), null);
            }
        }

        public CustomResponse RecusarCarga(MotoristaCarga carga) {
            _logger.LogDebug("[INFO] Executando funcao (Service): RecusarCarga Carga =>" + JsonSerializer.Serialize(carga));

            try {
                var CargaEscolhida = _repository.FindById(carga.CargaId);
                var MotoristaEscolhido = _motorista.FindById(carga.MotoristaId);

                if (CargaEscolhida == null) {
                    CustomHandler h = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.CARGA_NAO_ENCONTRADA);
                    throw new ApiCustomException(JsonSerializer.Serialize(h));
                }

                if (MotoristaEscolhido == null) {
                    CustomHandler h = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.MOTORISTA_NAO_ENCONTRADO);
                    throw new ApiCustomException(JsonSerializer.Serialize(h));
                }

                CargasRecusada newCR = new CargasRecusada(CargaEscolhida.Id, MotoristaEscolhido.Id);
                _repository.AddCargaRecusada(newCR);
                if (!_repository.SaveChanges()) {
                    CustomHandler h = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.CARGA_RECUSA_ERRO);
                    throw new ApiCustomException(JsonSerializer.Serialize(h));
                }
                return new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.RECUSA_ACEITA), null);
            } catch (ApiCustomException ex) {
                _logger.LogDebug("[ERRO] Ocorrencia de erro (Service): RecusarCarga Carga =>" + JsonSerializer.Serialize(ex.InnerException));
                CustomHandler RecuperaExcecao = JsonSerializer.Deserialize<CustomHandler>(ex.Message);
                return new CustomResponse(RecuperaExcecao.StatusCode, new CustomMessage(RecuperaExcecao.Nome, RecuperaExcecao.Descricao), null);
            }
        }

        public CustomResponse cargasRecusadas(int id) {
            _logger.LogDebug("[INFO] Executando funcao (Service): cargasRecusadas =>" + JsonSerializer.Serialize(id));

            try {
                var result = _repository.FindCargasRecusadas(id);

                return new CustomResponse(HttpStatusCode.OK, null, result);
            } catch (ApiCustomException ex) {
                CustomHandler RecuperaExcecao = JsonSerializer.Deserialize<CustomHandler>(ex.Message);
                return new CustomResponse(RecuperaExcecao.StatusCode, new CustomMessage(RecuperaExcecao.Nome, RecuperaExcecao.Descricao), null);
            }
        }

        public CustomResponse DeletarCarga(int id)
        {
            _logger.LogDebug("[INFO] Executando funcao (Service): DeletarCarga =>" + JsonSerializer.Serialize(id));
            try {
                Carga carga = _repository.FindById(id);
                if (carga == null) {
                    CustomHandler handler = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.CARGA_NAO_ENCONTRADA);
                    throw new ApiCustomException(JsonSerializer.Serialize(handler));
                } else {
                    var ListaCargas = _repository.FindCargaProdutos(carga.Id);
                    foreach (var item in ListaCargas) {
                        _repository.DeleteCargaProduto(item);
                    }
                    _repository.DeleteCarga(carga);
                }

                if (_repository.SaveChanges()) {
                    return new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.DELETAR_CARGA), null);
                }
                CustomHandler h = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.ERRO_DELETAR_CARGA);
                throw new ApiCustomException(JsonSerializer.Serialize(h));
            } catch (ApiCustomException ex) {
                _logger.LogDebug("[ERRO] Ocorrencia de erro (Service): DeleteCarga =>" + JsonSerializer.Serialize(ex.InnerException));
                CustomHandler RecuperaExcecao = JsonSerializer.Deserialize<CustomHandler>(ex.Message);
                return new CustomResponse(RecuperaExcecao.StatusCode, new CustomMessage(RecuperaExcecao.Nome, RecuperaExcecao.Descricao), null);
            }
        }

        private void AtualizaProdutos(ArrayList produtos) {
            foreach (var item in produtos) {
                _produto.Update((Produto)item);
            }
        }

        private void InsereCargaProdutos(IEnumerable<ProdutoQtd> produtos, int id) {
            foreach (var item in produtos) {
                _repository.AddCargaProdutos(new CargaProduto(id, item.ProdutoId, item.Qtd));
                _repository.SaveChanges();
            }

        }

        private void BuscaProdutos(Carga carga) {

            carga.Motorista = _motorista.FindById(carga.MotoristaId);
            var cargaProduto = _repository.FindCargaProdutos(carga.Id);
            List<ItemCargaProduto> produtos = new List<ItemCargaProduto>();
            foreach (var item in cargaProduto) {
                ItemCargaProduto itemCargaProduto = new ItemCargaProduto();

                itemCargaProduto.Produto = _produto.GetProdutoById(item.ProdutoId);
                itemCargaProduto.Quantidade = item.Qtd;

                produtos.Add(itemCargaProduto);
            }

            carga.ListaProdutos = produtos;
        }

    }
}
