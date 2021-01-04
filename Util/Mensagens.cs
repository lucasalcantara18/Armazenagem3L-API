using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Util {
    public static class Mensagens {
        public const string SUCESSO = "Sucesso";
        public const string ERRO = "Erro";
        public const string PRODUTO_ADD_SUCESSO = "Produto criado com sucesso!";
        public const string MOTORISTA_ADD_SUCESSO = "Motorista adicionado com sucesso!";
        public const string SENHA_ATT_SUCESSO = "Senha Alterada com sucesso!";
        public const string LOGIN_FALHA = "Login/Senha não correspondem!";
        public const string LOGIN_INEXISTENTE = "Não existe motorista com esse login!";
        public const string CARGA_ADD_SUCESSO = "Carga Adicionada com sucesso!";
        public const string CARGA_ADD_FALHA = "Erro no processo de criação de carga!";
        public const string CARGA_ACEITA = "A Carga foi atribuida com sucesso ao Motorista!";
        public const string RECUSA_ACEITA = "A Carga foi Recusada com sucesso ao Motorista!";
        public const string CARGA_ACEITA_ERRO = "Não foi possivel atribuir a carga ao Motorista!";
        public const string CARGA_RECUSA_ERRO = "Não foi possivel recusar a carga ao Motorista!";
        public const string ERRO_EMAIL = "Já existe um cadastro com esse email, tente novamente com outro!";
        public const string ERRO_LOGIN = "Já existe um cadastro com esse login, tente novamente com outro!";
        public const string ERRO_GERAL = "Não foi possível realizar a operação desejada, tente novamente mais tarde!";
        public const string ERRO_BUSCA_PRODUTO = "Não foi possível realizar a operação desejada, um produto da lista não existe!";
        public const string ERRO_SALVAR_CARGA = "A quatidade de um produto excede seu estoque!";
        public const string PRODUTO_NAO_ENCONTRADO = "Produto não encontrado";
        public const string DELETAR_PRODUTO = "Produto deletado com sucesso";
        public const string ERRO_DELETAR_PRODUTO = "Não foi possível deletar o produto";
        public const string CARGA_NAO_ENCONTRADA = "Carga não encontrada";
        public const string MOTORISTA_NAO_ENCONTRADO = "Motorista não encontrado";
        public const string ERRO_DELETAR_CARGA = "Não foi possível deletar a carga";
        public const string DELETAR_CARGA = "Carga deletada com sucesso";
    }
}