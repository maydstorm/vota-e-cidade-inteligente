using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VotaE_API.Controllers;
using VotaE_API.Interface;
using VotaE_API.Models;
using VotaE_API.ViewModel.Usuario;
using Xunit;

namespace VotaE_API.Testes.Controller
{
    public class UsuarioControllerTeste 
    {
        private readonly Mock<IUsuarioService> _usuarioServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UsuarioController _usuarioController;

        public UsuarioControllerTeste()
        {
            _usuarioServiceMock = new Mock<IUsuarioService>();
            _mapperMock = new Mock<IMapper>();
            _usuarioController = new UsuarioController(_usuarioServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void GetAllUsuariosTestes_Retorno_OK()
        {
            //Arrange
            var usuarios = new List<UsuarioModel>
            {
            new UsuarioModel { UsuarioId = 1, Nome = "Usuário 1" },
            new UsuarioModel { UsuarioId = 2, Nome = "Usuário 2" }
            };

            _usuarioServiceMock.Setup(service => service.GetAllUsuarios(It.IsAny<int>(), It.IsAny<int>())).Returns(usuarios);

            var usuariosViewModel = usuarios.Select(u => new UsuarioViewModel { UsuarioId = u.UsuarioId, Nome = u.Nome });
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<UsuarioViewModel>>(It.IsAny<IEnumerable<UsuarioModel>>()))
                .Returns(usuariosViewModel);

            // Act
            var result = _usuarioController.GetAllUsuarios(0,10);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var viewModel = Assert.IsType<UsuarioPaginacaoViewModel>(okResult.Value);

            Assert.Equal(2, viewModel.Usuarios.Count());
            Assert.Equal(10, viewModel.PageSize);
            Assert.Equal(2, viewModel.NextRef);
        }

        [Fact]
        public void GetAllUsuarios_Retorno_NotFound()
        {
            // Arrange
            _usuarioServiceMock.Setup(service => service.GetAllUsuarios(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<UsuarioModel>());

            // Act
            var result = _usuarioController.GetAllUsuarios(0, 10);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }


    }
}
