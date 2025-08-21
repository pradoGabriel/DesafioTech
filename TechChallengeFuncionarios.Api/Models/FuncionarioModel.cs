using System.ComponentModel.DataAnnotations;
using TechChallengeFuncionarios.Api.Enums;

namespace TechChallengeFuncionarios.Api.Models
{
    public class FuncionarioModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Sobrenome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string Documento { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        public RoleEnum Role { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string NomeGerente { get; set; }

        public List<FuncionarioTelefoneModel> Telefones { get; set; }
    }
}
