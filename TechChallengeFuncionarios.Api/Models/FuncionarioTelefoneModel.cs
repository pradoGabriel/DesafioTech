using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TechChallengeFuncionarios.Api.Models
{
    public class FuncionarioTelefoneModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Numero { get; set; }
        [ForeignKey("FuncionarioId")]
        public int FuncionarioId { get; set; }
    }
}
