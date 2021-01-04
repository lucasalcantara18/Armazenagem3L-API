using Armazenagem3L_API.Data;
using Armazenagem3L_API.Logger;
using Armazenagem3L_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;

namespace Armazenagem3L_API.Repositories.impl {
    public class ProdutosRepositoryImpl : IProdutosRepository {
        
        private readonly DataContext _context;
        private readonly ILoggerManager _logger;

        public ProdutosRepositoryImpl(DataContext context, ILoggerManager logger) {
            _context = context;
            _logger = logger;
        }

        public void Add(Produto produto) {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): Add Produto =>" + JsonSerializer.Serialize(produto));
            _context.Add(produto);
        }

        public Produto GetProdutoById(int produtoId) {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): GetProdutoById Produto =>" + JsonSerializer.Serialize(produtoId));
            IQueryable<Produto> query = _context.Produto;

            return query.AsNoTracking().OrderBy(p => p.Id).Where(produto => produto.Id == produtoId)
                    .FirstOrDefault();
        }

        public Produto[] GetProdutos() {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): GetProdutos");
            IQueryable<Produto> query = _context.Produto;

            return  query.AsNoTracking().OrderBy(p => p.Id).ToArray();
        }

        public void Delete(Produto produto) {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): Delete Produto =>" + JsonSerializer.Serialize(produto));
            _context.Remove(produto);
        }

        public void Update(Produto produto) {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): Update Produto =>" + JsonSerializer.Serialize(produto));            
            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();
        }
        
        public bool SaveChanges() {
            return (_context.SaveChanges() > 0);
        }

    }
}
