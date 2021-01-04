using Armazenagem3L_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Repositories {
    public interface IProdutosRepository {

        Produto[] GetProdutos();

        Produto GetProdutoById(int produtoId);

        void Add(Produto produto);

        void Update(Produto produto);
        
        void Delete(Produto produto);
        
        bool SaveChanges();
    }
}
