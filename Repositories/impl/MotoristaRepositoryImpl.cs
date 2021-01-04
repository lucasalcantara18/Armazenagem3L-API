using Armazenagem3L_API.Data;
using Armazenagem3L_API.Logger;
using Armazenagem3L_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;

namespace Armazenagem3L_API.Repositories.impl {
    public class MotoristaRepositoryImpl : IMotoristaRepository {
        
        private readonly DataContext _context;
        private readonly ILoggerManager _logger;

        public MotoristaRepositoryImpl(DataContext context, ILoggerManager logger) {
            _context = context;
            _logger = logger;
        }

        public void Add(Motorista motorista) {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): Add Motorista =>" + JsonSerializer.Serialize(motorista));
            _context.Add(motorista);
        }

        public void Update(Motorista motorista) {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): Update Motorista =>" + JsonSerializer.Serialize(motorista));
            _context.Entry(motorista).State = EntityState.Modified;
        }

        public Motorista FindById(int Id) {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): FindById Motorista =>" + JsonSerializer.Serialize(Id));
            return _context.Motorista.AsNoTracking().OrderBy(p => p.Id).Where(m => m.Id == Id).FirstOrDefault();
        }

        public Motorista FindByLogin(string login) {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): FindByLogin Motorista =>" + JsonSerializer.Serialize(login));
            return _context.Motorista.AsNoTracking().OrderBy(p => p.Id).Where(m => m.Login == login).FirstOrDefault();
        }

        public Motorista FindByEmail(string email) {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): FindByEmail Motorista =>" + JsonSerializer.Serialize(email));
            return _context.Motorista.AsNoTracking().OrderBy(p => p.Id).Where(m => m.Email == email).FirstOrDefault();
        }

        public bool SaveChanges() {
            return (_context.SaveChanges() > 0);
        }

    }
}
