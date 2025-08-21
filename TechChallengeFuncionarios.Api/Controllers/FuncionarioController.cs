using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TechChallengeFuncionarios.Api.Models;
using TechChallengeFuncionarios.Api.Services.Interfaces;

namespace TechChallengeFuncionarios.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FuncionarioController : ControllerBase
    {
        #region Propriedades
        private readonly IFuncionarioService _service;
        private readonly Serilog.ILogger _logger = Serilog.Log.ForContext<FuncionarioController>();
        #endregion

        #region Construtor
        public FuncionarioController(IFuncionarioService service)
        {
            _service = service;
        }
        #endregion

        #region Métodos Públicos
        [HttpGet]
        [SwaggerOperation(Summary = "Lista todos os funcionários", Description = "Retorna uma lista de todos os funcionários.")]
        [ProducesResponseType(typeof(IEnumerable<FuncionarioModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<FuncionarioModel>>> Getfuncionarios()
        {
            _logger.Information("Listando todos os funcionários");
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Busca funcionário por ID", Description = "Retorna um funcionário específico pelo ID.")]
        [ProducesResponseType(typeof(FuncionarioModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FuncionarioModel>> GetFuncionario(int id)
        {
            var funcionario = await _service.GetByIdAsync(id);
            if (funcionario == null)
            {
                _logger.Warning("Funcionário não encontrado: {Id}", id);
                return NotFound();
            }
            _logger.Information("Funcionário encontrado: {Id}", id);
            return Ok(funcionario);
        }


        [HttpPost]
        [Authorize(Roles = "Lider, Diretor")]
        [SwaggerOperation(Summary = "Cria novo funcionário", Description = "Adiciona um novo funcionário.")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(FuncionarioModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FuncionarioModel>> CreateFuncionario(FuncionarioModel funcionario)
        {
            var role = User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role).Select(c => c.Value).FirstOrDefault();
            if (role == "Lider" && funcionario.Role == Enums.RoleEnum.Diretor)
            {
                _logger.Warning("Você não tem permissão para criar este tipo de funcionário.", role);
                return Forbid();
            }

            var (success, error) = await _service.CreateAsync(funcionario);
            if (!success)
            {
                _logger.Warning("Falha ao criar funcionário: {Error}", error);
                return BadRequest(error);
            }
            _logger.Information("Funcionário criado: {Email}", funcionario.Email);
            return CreatedAtAction(nameof(GetFuncionario), new { id = funcionario.Id }, funcionario);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Lider,Diretor")]
        [SwaggerOperation(Summary = "Atualiza funcionário", Description = "Atualiza os dados de um funcionário existente.")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateFuncionario(int id, FuncionarioModel funcionario)
        {
            var role = User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role).Select(c => c.Value).FirstOrDefault();
            if (role == "Lider" && funcionario.Role == Enums.RoleEnum.Diretor)
            {
                _logger.Warning("Você não tem permissão para atualizar este tipo de funcionário.", role);
                return Forbid();
            }
            var (success, error) = await _service.UpdateAsync(id, funcionario);
            if (!success)
            {
                _logger.Warning("Falha ao atualizar funcionário: {Error}", error);
                return BadRequest(error);
            }
            _logger.Information("Funcionário atualizado: {Email}", funcionario.Email);
            return NoContent();
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Diretor")]
        [SwaggerOperation(Summary = "Exclui funcionário", Description = "Remove um funcionário pelo ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteFuncionario(int id)
        {
            var (success, error) = await _service.DeleteAsync(id);
            if (!success)
            {
                _logger.Warning("Falha ao excluir funcionário: {Error}", error);
                return BadRequest(error);
            }
            _logger.Information("Funcionário excluído: {Id}", id);
            return NoContent();
        }
        #endregion
    }
}
