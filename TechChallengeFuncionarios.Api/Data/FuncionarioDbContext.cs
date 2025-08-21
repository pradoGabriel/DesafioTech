using Microsoft.EntityFrameworkCore;
using TechChallengeFuncionarios.Api.Models;

namespace TechChallengeFuncionarios.Api.Data
{
    public class FuncionarioDbContext : DbContext
    {
        public FuncionarioDbContext(DbContextOptions<FuncionarioDbContext> options) : base(options) { }

        public DbSet<FuncionarioModel> Funcionarios { get; set; }
        public DbSet<FuncionarioTelefoneModel> FuncionarioTelefone { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FuncionarioModel>()
                .HasIndex(e => e.Documento)
                .IsUnique();

            modelBuilder.Entity<FuncionarioModel>()
                .HasMany(e => e.Telefones)
                .WithOne()
                .HasForeignKey(p => p.FuncionarioId);
        }
    }
}
