using Armazenagem3L_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Armazenagem3L_API.Data {
    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Carga> Cargas { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Motorista> Motorista { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<CargaProduto> CargaProdutos { get; set; }
        public DbSet<CargasRecusada> CargasRecusadas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<CargaProduto>().HasKey(AD => new { AD.CargaId, AD.ProdutoId });

            modelBuilder.Entity<CargasRecusada>().HasKey(AD => new { AD.CargaId, AD.MotoristaId });

            modelBuilder.Entity<Funcionario>()
                    .HasData(new List<Funcionario>(){
                    new Funcionario(1, "Lauro"),
                    });

            modelBuilder.Entity<Motorista>()
                    .HasData(new List<Motorista>(){
                    new Motorista(1, "Bino", "bino", "cargapesada", "bino_cilada@gmail.com"),
                    });

            modelBuilder.Entity<Produto>()
                    .HasData(new List<Produto>(){
                    new Produto(1, "Playstation 5", 1, 1, 300),
                    new Produto(2, "Mouse", 1, 1, 300),
                    new Produto(3, "Teclado", 1, 1, 300),
                    new Produto(4, "Monitor", 1, 1, 300),
                    new Produto(5, "Dualshock 4", 1, 1, 300),
                    new Produto(6, "Dualsense", 1, 1, 300),
                    });
        }
    }
}
