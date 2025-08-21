using Microsoft.EntityFrameworkCore;
using TechChallengeFuncionarios.Api.Data;
using TechChallengeFuncionarios.Api.Models;
using TechChallengeFuncionarios.Api.Repositories.Interfaces;

namespace TechChallengeFuncionarios.Api.Repositories
{
    public class FuncionarioRepositorio : IFuncionarioRepositorio
    {
        #region Propriedades
        private readonly FuncionarioDbContext _context;
        #endregion

        #region Construtor
        public FuncionarioRepositorio(FuncionarioDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Métodos Públicos
        public async Task<IEnumerable<FuncionarioModel>> GetAllAsync()
        {
            return await _context.Funcionarios.Include(e => e.Telefones).ToListAsync();
        }

        public async Task<FuncionarioModel?> GetByIdAsync(int id)
        {
            return await _context.Funcionarios.Include(e => e.Telefones).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<FuncionarioModel?> GetByDocumentoAsync(string documento)
        {
            return await _context.Funcionarios.FirstOrDefaultAsync(e => e.Documento == documento);
        }

        public async Task<FuncionarioModel?> GetByEmailAsync(string email)
        {
            return await _context.Funcionarios.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task AddAsync(FuncionarioModel funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FuncionarioModel funcionario)
        {
            _context.Entry(funcionario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario != null)
            {
                _context.Funcionarios.Remove(funcionario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsDocumentoAsync(string documento, int? desconsiderarId = null)
        {
            return await _context.Funcionarios.AnyAsync(e => e.Documento == documento && (!desconsiderarId.HasValue || e.Id != desconsiderarId.Value));
        }
        #endregion
    }
}
