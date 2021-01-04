using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Armazenagem3L_API.ExceptionHandler {
    public class CustomHandler {
        public HttpStatusCode StatusCode { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public CustomHandler(HttpStatusCode statusCode, string nome, string descricao) {
            StatusCode = statusCode;
            Nome = nome;
            Descricao = descricao;
        }
    }
}
