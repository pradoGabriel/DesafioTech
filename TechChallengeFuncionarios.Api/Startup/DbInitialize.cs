using TechChallengeFuncionarios.Api.Data;
using TechChallengeFuncionarios.Api.Models;

namespace TechChallengeFuncionarios.Api.Startup
{
    public static class DbInitialize
    {
        public static void SeedDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<FuncionarioDbContext>();

            // Lógica de inserção de dados
            if (!context.Funcionarios.Any())
            {
                context.Funcionarios.Add(new FuncionarioModel 
                { 
                    Nome = "Admin", 
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123"),
                    Sobrenome = "Admin",
                    Email = "admin@admin.com.br",
                    Documento = "00000000000",
                    DataNascimento = DateTime.Now,
                    Role = Enums.RoleEnum.Diretor,
                    NomeGerente = "Admin",
                    Telefones = new List<FuncionarioTelefoneModel>
                    {
                        new FuncionarioTelefoneModel { Numero = "11999999999" }
                    }

                });
                context.SaveChanges();
            }
        }
    }
}
