using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Models {
    public class MotoristaCarga {

        public int CargaId { get; set; }
        public int MotoristaId { get; set; }

        public MotoristaCarga(int cargaId, int motoristaId) {
            CargaId = cargaId;
            MotoristaId = motoristaId;
        }

        public MotoristaCarga() {
        }
    }
}
