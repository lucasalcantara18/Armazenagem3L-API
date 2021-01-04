using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Models {
    [Table("Produtos", Schema = "Armazenagem3L")]
    public class Produto {

        public Produto() { }

        public Produto(int id, string nome, decimal peso, decimal preco, int qtd) {
            Id = id;
            Nome = nome;
            Peso = peso;
            Preco = preco;
            Qtd = qtd;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Nome Faltando")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Peso Faltando")]
        [Column(TypeName = "decimal(8, 3)")]
        public decimal Peso { get; set; }

        [Required(ErrorMessage = "Campo Preço Faltando")]
        [DataType(DataType.Currency, ErrorMessage = "O Valor do campo esta fora do padrão (18,2)")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Campo Faltando")]
        [Range(1, 999, ErrorMessage = "O valor deve esta entre 9 e 999")]
        public int Qtd { get; set; }

    }
}
