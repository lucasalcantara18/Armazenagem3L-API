using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Models {
    [Table("Funcionarios", Schema = "Armazenagem3L")]
    public class Funcionario {

        public Funcionario() { }

        public Funcionario(int Id, string Nome) {
            this.Id = Id;
            this.Nome = Nome;
        }

        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }

    }
}
