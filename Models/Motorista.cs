using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Models {
    [Table("Motoristas", Schema = "Armazenagem3L")]
    public class Motorista {
        public Motorista() { }

        public Motorista(int id, string nome) {
            Id = id;
            Nome = nome;
        }

        public Motorista(int id, string nome, string login, string senha, string email) {
            Id = id;
            Nome = nome;
            Login = login;
            Senha = senha;
            Email = email;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Nome Faltando")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Login Faltando")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Campo Senha Faltando")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Campo Email Faltando")]
        public string Email { get; set; }
    }
}
