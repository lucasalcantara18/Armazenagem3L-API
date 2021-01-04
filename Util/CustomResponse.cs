using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Util {
    public class CustomResponse {

        public HttpStatusCode StatusCode { get; set; }
        public CustomMessage Mensagem { get; set; }
        public Object Objeto { get; set; }
        public Object Retorno { get; set; }

        public CustomResponse() {

        }

        public CustomResponse(HttpStatusCode StatusCode, CustomMessage Mensagem, Object Objeto) {
            this.StatusCode = StatusCode;
            this.Mensagem = Mensagem;
            this.Objeto = Objeto;

            if(Mensagem != null) {
                this.Retorno = this.Mensagem;
            } else {
                this.Retorno = this.Objeto;
            }

        }

    }
}
