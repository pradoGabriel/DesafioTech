using TechChallengeFuncionarios.Api.Models;

namespace TechChallengeFuncionarios.Api.Services.Interfaces
{
    public interface IFuncionarioService
    {
        Task<IEnumerable<FuncionarioModel>> GetAllAsync();
        Task<FuncionarioModel?> GetByIdAsync(int id);
        Task<(bool Success, string Error)> CreateAsync(FuncionarioModel funcionario);
        Task<(bool Success, string Error)> UpdateAsync(int id, FuncionarioModel funcionario);
        Task<(bool Success, string Error)> DeleteAsync(int id);
    }
}
