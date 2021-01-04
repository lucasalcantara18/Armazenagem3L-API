using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Models {
    public class ProdutoQtd {

        public int ProdutoId { get; set; }

        public int Qtd { get; set; }

        public int CargaId { get; set; }

        public ProdutoQtd() {
        }

        public ProdutoQtd(int produtoId, int qtd, int cargaId) {
            ProdutoId = produtoId;
            Qtd = qtd;
            CargaId = cargaId;
        }
    }
}
