using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechChallengeFuncionarios.Api.Data;
using TechChallengeFuncionarios.Api.Models;

namespace TechChallengeFuncionarios.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        #region Propriedades
        private readonly FuncionarioDbContext _context;
        private readonly IConfiguration _config;
        #endregion

        #region Construtor
        public AuthController(FuncionarioDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        #endregion

        #region Métodos Públicos
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(e => e.Email == request.Email);
            if (funcionario == null || !BCrypt.Net.BCrypt.Verify(request.Password, funcionario.PasswordHash))
                return Unauthorized("Credenciais Inválidas");

            var token = GenerateJwtToken(funcionario);
            return Ok(new { token });
        }
        #endregion

        #region Métodos Privados
        private string GenerateJwtToken(FuncionarioModel funcionario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, funcionario.Id.ToString()),
                new Claim(ClaimTypes.Name, funcionario.Email),
                new Claim(ClaimTypes.Role, funcionario.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
