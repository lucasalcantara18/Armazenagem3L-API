using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Models {
    [Table("Cargas", Schema = "Armazenagem3L")]
    public class Carga {

        public Carga() { }

        public Carga(string endereco, decimal frete, int motoristaId, IEnumerable<ProdutoQtd> produtos) {
            Endereco = endereco;
            Frete = frete;
            MotoristaId = motoristaId;
            Produtos = produtos;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Endereço faltando")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Campo Frete faltando")]
        public decimal Frete { get; set; }

        public int MotoristaId { get; set; }

        [NotMapped]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<ProdutoQtd> Produtos { get; set; }

        [NotMapped]
        public List<ItemCargaProduto> ListaProdutos { get; set; }


        [NotMapped]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Motorista Motorista { get; set; }
    }
}
