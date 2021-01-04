using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Util {
    public class CustomMessage {
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public CustomMessage(string Nome, string Descricao) {
            this.Nome = Nome;
            this.Descricao = Descricao;
        }

        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }
    }
}
