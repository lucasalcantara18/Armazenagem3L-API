using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Models {
    [Table("CargaProdutos", Schema = "Armazenagem3L")]
    public class CargaProduto {
        public CargaProduto(int cargaId, int produtoId, int Qtd) {
            CargaId = cargaId;
            ProdutoId = produtoId;
            this.Qtd = Qtd;
        }

        public CargaProduto() {}

        [Key, Column(Order = 1)]
        public int CargaId { get; set; }

        [Key, Column(Order = 1)]
        public int ProdutoId { get; set; }

        public int Qtd { get; set; }

        public Produto Produto { get; set; }

        public Carga Carga { get; set; }
    }
}
