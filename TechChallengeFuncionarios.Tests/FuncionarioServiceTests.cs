using Moq;
using TechChallengeFuncionarios.Api.Models;
using TechChallengeFuncionarios.Api.Services.Interfaces;

namespace TechChallengeFuncionarios.Tests
{
    public class FuncionarioServiceTests
    {
        private readonly Mock<IFuncionarioService> _serviceMock;

        public FuncionarioServiceTests()
        {
            _serviceMock = new Mock<IFuncionarioService>();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsFuncionarios()
        {
            var funcionarios = new List<FuncionarioModel>
            {
                new FuncionarioModel { Id = 1, Nome = "João" },
                new FuncionarioModel { Id = 2, Nome = "Maria" }
            };

            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(funcionarios);

            var result = await _serviceMock.Object.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, ((List<FuncionarioModel>)result).Count);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsFuncionario()
        {
            var funcionario = new FuncionarioModel { Id = 1, Nome = "João" };
            _serviceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(funcionario);
          
            var result = await _serviceMock.Object.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task CreateAsync_ReturnsSuccess()
        {
            var funcionario = new FuncionarioModel { Id = 3, Nome = "Carlos" };
            _serviceMock.Setup(s => s.CreateAsync(funcionario)).ReturnsAsync((true, string.Empty));
          
            var (success, error) = await _serviceMock.Object.CreateAsync(funcionario);

            Assert.True(success);
            Assert.Equal(string.Empty, error);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsError()
        {
            var funcionario = new FuncionarioModel { Id = 1, Nome = "João" };
            _serviceMock.Setup(s => s.UpdateAsync(1, funcionario)).ReturnsAsync((false, "Not found"));
         
            var (success, error) = await _serviceMock.Object.UpdateAsync(1, funcionario);

            Assert.False(success);
            Assert.Equal("Not found", error);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsSuccess()
        {
            _serviceMock.Setup(s => s.DeleteAsync(1)).ReturnsAsync((true, string.Empty));
         
            var (success, error) = await _serviceMock.Object.DeleteAsync(1);

            Assert.True(success);
            Assert.Equal(string.Empty, error);
        }
    }
}