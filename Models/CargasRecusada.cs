using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Models {
    [Table("CargasRecusadas", Schema = "Armazenagem3L")]
    public class CargasRecusada {

        public CargasRecusada() {}

        public CargasRecusada(int cargaId, int motoristaId) {
            CargaId = cargaId;
            MotoristaId = motoristaId;
        }

        [Key, Column(Order = 1)]
        public int CargaId { get; set; }

        [Key, Column(Order = 1)]
        public int MotoristaId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Motorista Motorista { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Carga Carga { get; set; }
    }
}
