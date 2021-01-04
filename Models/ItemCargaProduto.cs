using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Models
{
    public class ItemCargaProduto
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
    }
}
