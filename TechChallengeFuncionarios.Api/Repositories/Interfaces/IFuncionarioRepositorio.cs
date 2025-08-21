using TechChallengeFuncionarios.Api.Models;

namespace TechChallengeFuncionarios.Api.Repositories.Interfaces
{
    public interface IFuncionarioRepositorio
    {
        Task<IEnumerable<FuncionarioModel>> GetAllAsync();
        Task<FuncionarioModel?> GetByIdAsync(int id);
        Task<FuncionarioModel?> GetByDocumentoAsync(string documento);
        Task<FuncionarioModel?> GetByEmailAsync(string email);
        Task AddAsync(FuncionarioModel funcionario);
        Task UpdateAsync(FuncionarioModel funcionario);
        Task DeleteAsync(int id);
        Task<bool> ExistsDocumentoAsync(string documento, int? desconsiderarId = null);
    }
}
