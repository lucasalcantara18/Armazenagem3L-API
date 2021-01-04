using Armazenagem3L_API.ExceptionHandler;
using Armazenagem3L_API.Logger;
using Armazenagem3L_API.Models;
using Armazenagem3L_API.Repositories;
using Armazenagem3L_API.Util;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace Armazenagem3L_API.Services {
    public class MotoristaService {
        
        private readonly IMotoristaRepository _repository;
        private readonly ILoggerManager _logger;

        public MotoristaService(IMotoristaRepository repository, ILoggerManager logger) {
            _repository = repository;
            _logger = logger;
        }
        
        public CustomResponse Add(Motorista motorista) {
            _logger.LogDebug("[INFO] Executando funcao (Service): Add Motorista =>" + JsonSerializer.Serialize(motorista));
            try {

                var motoristaAux = _repository.FindByEmail(motorista.Email);

                if (motoristaAux != null) {
                    CustomHandler handler = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.ERRO_EMAIL);
                    throw new ApiCustomException(JsonSerializer.Serialize(handler));
                }

                motoristaAux = _repository.FindByLogin(motorista.Login);

                if (motoristaAux != null) {
                    CustomHandler handler = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.ERRO_LOGIN);
                    throw new ApiCustomException(JsonSerializer.Serialize(handler));
                }

                _repository.Add(motorista);
                if (!_repository.SaveChanges()) {
                    CustomHandler handler = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.ERRO_GERAL);
                    throw new ApiCustomException(JsonSerializer.Serialize(handler));
                }
                return new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.MOTORISTA_ADD_SUCESSO), null);
            } catch (ApiCustomException ex) {
                _logger.LogDebug("[ERRO] Ocorrencia de erro (Service): Add Motorista =>" + JsonSerializer.Serialize(ex.InnerException));
                CustomHandler RecuperaExcecao = JsonSerializer.Deserialize<CustomHandler>(ex.Message);
                return new CustomResponse(RecuperaExcecao.StatusCode, new CustomMessage(RecuperaExcecao.Nome, RecuperaExcecao.Descricao), null);
            }
        }

        public CustomResponse RecuperarSenha(DadosMotorista value) {
            _logger.LogDebug("[INFO] Executando funcao (Service): RecuperarSenha  =>" + JsonSerializer.Serialize(value));

            try {
                Motorista motorista = _repository.FindByEmail(value.Email);

                if (motorista == null) {
                    CustomHandler h = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.MOTORISTA_NAO_ENCONTRADO);
                    throw new ApiCustomException(JsonSerializer.Serialize(h));
                }

                motorista.Senha = value.Senha;
                _repository.Update(motorista);

                if (!_repository.SaveChanges()) {
                    CustomHandler handler = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.ERRO_GERAL);
                    throw new ApiCustomException(JsonSerializer.Serialize(handler));
                }

                return new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.SUCESSO, Mensagens.SENHA_ATT_SUCESSO), null);
            } catch (ApiCustomException ex) {
                CustomHandler RecuperaExcecao = JsonSerializer.Deserialize<CustomHandler>(ex.Message);
                return new CustomResponse(RecuperaExcecao.StatusCode, new CustomMessage(RecuperaExcecao.Nome, RecuperaExcecao.Descricao), null);
            }
        }
        public CustomResponse VerificarEmail(DadosMotorista value)
        {
            _logger.LogDebug("[INFO] Executando funcao (Service): RecuperarSenha  =>" + JsonSerializer.Serialize(value));

            try
            {
                Motorista motorista = _repository.FindByEmail(value.Email);

                if (motorista == null)
                {
                    return new CustomResponse(HttpStatusCode.OK, null, false);
                }

                return new CustomResponse(HttpStatusCode.OK, null, true);
            }
            catch (ApiCustomException ex)
            {
                CustomHandler RecuperaExcecao = JsonSerializer.Deserialize<CustomHandler>(ex.Message);
                return new CustomResponse(RecuperaExcecao.StatusCode, new CustomMessage(RecuperaExcecao.Nome, RecuperaExcecao.Descricao), null);
            }
        }

        public CustomResponse login(DadosMotorista value) {
            _logger.LogDebug("[INFO] Executando funcao (Service): Login  =>" + JsonSerializer.Serialize(value));

            try {
                Motorista motorista = _repository.FindByLogin(value.Login);

                if (motorista == null) {
                    return new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.ERRO, Mensagens.LOGIN_INEXISTENTE), null);
                }

                if(value.Senha == motorista.Senha) {
                    return new CustomResponse(HttpStatusCode.OK, null, true);
                }

                return new CustomResponse(HttpStatusCode.OK, new CustomMessage(Mensagens.ERRO, Mensagens.LOGIN_FALHA), null);
            } catch (ApiCustomException ex) {
                CustomHandler RecuperaExcecao = JsonSerializer.Deserialize<CustomHandler>(ex.Message);
                return new CustomResponse(RecuperaExcecao.StatusCode, new CustomMessage(RecuperaExcecao.Nome, RecuperaExcecao.Descricao), null);
            }
        }

        public CustomResponse FindById(int id) {
            _logger.LogDebug("[INFO] Executando funcao (Service): FindById  Motorista =>" + JsonSerializer.Serialize(id));

            try {
                Motorista motorista = _repository.FindById(id);

                if (motorista == null) {
                    CustomHandler h = new CustomHandler(HttpStatusCode.UnprocessableEntity, Mensagens.ERRO, Mensagens.MOTORISTA_NAO_ENCONTRADO);
                    throw new ApiCustomException(JsonSerializer.Serialize(h));
                } 

                return new CustomResponse(HttpStatusCode.OK, null, motorista);
            } catch (ApiCustomException ex) {
                CustomHandler RecuperaExcecao = JsonSerializer.Deserialize<CustomHandler>(ex.Message);
                return new CustomResponse(RecuperaExcecao.StatusCode, new CustomMessage(RecuperaExcecao.Nome, RecuperaExcecao.Descricao), null);
            }
        }

    }
}
