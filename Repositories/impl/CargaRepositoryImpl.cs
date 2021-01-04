using Armazenagem3L_API.Data;
using Armazenagem3L_API.Logger;
using Armazenagem3L_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;


namespace Armazenagem3L_API.Repositories.impl {
    public class CargaRepositoryImpl : ICargaRepository {

        private readonly DataContext _context;
        private readonly ILoggerManager _logger;

        public CargaRepositoryImpl(DataContext context, ILoggerManager logger) {
            _context = context;
            _logger = logger;
        }

        public void Add(Carga carga) {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): Add Carga =>" + JsonSerializer.Serialize(carga));
            _context.Add(carga);
        }

        public void AddCargaProdutos(CargaProduto cargaProduto) {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): AddCargaProdutos =>" + JsonSerializer.Serialize(cargaProduto));
            _context.Add(cargaProduto);
        }

        public Carga cargaByIdAndMotoristaId(int cargaId, int motoristaId) {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): cargaByIdAndMotoristaId => carga " + JsonSerializer.Serialize(cargaId) + " motorista " + JsonSerializer.Serialize(motoristaId));
            IQueryable<Carga> query = _context.Cargas;

            return query.AsNoTracking().OrderBy(c => c.Id)
                .Where(carga => (carga.Id == cargaId) && (carga.MotoristaId == motoristaId)).FirstOrDefault();
        }

        public Carga[] cargaByMotoristaId(int motoristaId) {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): cargaByMotoristaId => " + JsonSerializer.Serialize(motoristaId));
            IQueryable<Carga> query = _context.Cargas;

            return query.AsNoTracking().OrderBy(c => c.Id).Where(carga => carga.MotoristaId == motoristaId).ToArray();
        }

        public void DeleteCarga(Carga carga) {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): DeleteCarga =>" + JsonSerializer.Serialize(carga));
            _context.Remove(carga);
        }

        public void DeleteCargaProduto(CargaProduto cargaProduto) {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): DeleteCargaProdutos =>" + JsonSerializer.Serialize(cargaProduto));
            _context.Remove(cargaProduto);
        }
        
        public Carga FindById(int Id) {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): FindById Carga =>" + JsonSerializer.Serialize(Id));
           return _context.Cargas.AsNoTracking().OrderBy(p => p.Id).Where(c => c.Id == Id).FirstOrDefault();
        }

        public Carga GetCargaById(int cargaId) {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): GetCargaById => " + JsonSerializer.Serialize(cargaId));
            IQueryable<Carga> query = _context.Cargas;
            IQueryable<Motorista> queryMotorista = _context.Motorista;

            Carga carga = query.AsNoTracking().OrderBy(c => c.Id).Where(carga => carga.Id == cargaId).FirstOrDefault();

            Motorista motorista = queryMotorista.AsNoTracking().OrderBy(m => m.Id).Where(motorista => motorista.Id == carga.MotoristaId).FirstOrDefault();

            carga.Motorista = motorista;

            return carga;
        }

        public Carga[] GetCargas() {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): GetProdutos");
            IQueryable<Carga> query = _context.Cargas;

            return query.AsNoTracking().OrderBy(c => c.Id).ToArray();
        }

        public IEnumerable<CargaProduto> FindCargaProdutos(int Id) {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): FindCargaProdutos =>" + JsonSerializer.Serialize(Id));
            return _context.CargaProdutos.AsNoTracking().OrderBy(p => p.CargaId).Where(c => c.CargaId == Id).ToList();
        }
        
        public Carga GetLast() {
            return _context.Cargas.OrderBy(c => c.Id).Last();
        }

        public bool SaveChanges() {
            return (_context.SaveChanges() > 0);
        }

        public void Update(Carga carga) {
            _context.Entry(carga).State = EntityState.Modified;
        }

        public void AddCargaRecusada(CargasRecusada cargaRecusada) {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): AddCargaRecusada =>" + JsonSerializer.Serialize(cargaRecusada));
            _context.Add(cargaRecusada);
        }

        public IEnumerable<CargasRecusada> FindCargasRecusadas(int Id) {
            _logger.LogDebug("[INFO] Executando CRUD no banco de dados: (Repository): FindCargaProdutos =>" + JsonSerializer.Serialize(Id));
            return _context.CargasRecusadas.AsNoTracking().OrderBy(p => p.CargaId).Where(c => c.MotoristaId == Id).ToList();
        }
    }
}
