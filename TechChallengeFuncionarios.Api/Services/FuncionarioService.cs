using TechChallengeFuncionarios.Api.Models;
using TechChallengeFuncionarios.Api.Repositories.Interfaces;
using TechChallengeFuncionarios.Api.Services.Interfaces;

namespace TechChallengeFuncionarios.Api.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        #region Propriedades
        private readonly IFuncionarioRepositorio _repository;
        #endregion

        #region Construtor
        public FuncionarioService(IFuncionarioRepositorio repository)
        {
            _repository = repository;
        }
        #endregion

        #region Métodos Públicos
        public async Task<IEnumerable<FuncionarioModel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<FuncionarioModel?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<(bool Success, string Error)> CreateAsync(FuncionarioModel funcionario)
        {
            var idade = DateTime.Today.Year - funcionario.DataNascimento.Year;
            if (funcionario.DataNascimento.Date > DateTime.Today.AddYears(-idade)) idade--;
            if (idade < 18)
                return (false, "O Funcionário deve ser maior de 18.");

            if (await _repository.ExistsDocumentoAsync(funcionario.Documento))
                return (false, "Documento já cadastrado.");

            if (funcionario.Telefones == null || funcionario.Telefones.Count < 2)
                return (false, "Funcionário deve ter pelo menos 2 números de telefone.");

            funcionario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(funcionario.PasswordHash);

            await _repository.AddAsync(funcionario);
            return (true, string.Empty);
        }

        public async Task<(bool Success, string Error)> UpdateAsync(int id, FuncionarioModel funcionario)
        {

            var idade = DateTime.Today.Year - funcionario.DataNascimento.Year;
            if (funcionario.DataNascimento.Date > DateTime.Today.AddYears(-idade)) idade--;
            if (idade < 18)
                return (false, "O Funcionário deve ser maior de 18.");

            if (await _repository.ExistsDocumentoAsync(funcionario.Documento, id))
                return (false, "Documento deve ser único.");

            if (funcionario.Telefones == null || funcionario.Telefones.Count < 2)
                return (false, "Funcionário deve ter pelo menos 2 números de telefone.");

            funcionario.Id = id;
            await _repository.UpdateAsync(funcionario);
            return (true, string.Empty);
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            return (true, string.Empty);
        }
        #endregion
    }
}
