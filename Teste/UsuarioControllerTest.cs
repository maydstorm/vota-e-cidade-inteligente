using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VotaE_API.Controllers;
using VotaE_API.Interface;
using VotaE_API.Models;
using VotaE_API.ViewModel.Usuario;

namespace Teste
{
    public class UsuarioControllerTest
    {
        private readonly Mock<IUsuarioService> _usuarioServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UsuarioController _usuarioController;

        public UsuarioControllerTest()
        {
            _usuarioServiceMock = new Mock<IUsuarioService>();
            _mapperMock = new Mock<IMapper>();
            _usuarioController = new UsuarioController(_usuarioServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void GetAllUsuarios_Return_OK()
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
            var result = _usuarioController.GetAllUsuarios(0, 10);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var viewModel = Assert.IsType<UsuarioPaginacaoViewModel>(okResult.Value);

            Assert.Equal(2, viewModel.Usuarios.Count());
            Assert.Equal(10, viewModel.PageSize);
            Assert.Equal(2, viewModel.NextRef);
        }

        [Fact]
        public void GetAllUsuarios_Return_NotFound()
        {
            // Arrange
            _usuarioServiceMock.Setup(service => service.GetAllUsuarios(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<UsuarioModel>());

            // Act
            var result = _usuarioController.GetAllUsuarios(0, 10);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public void GetUsuarioById_Return_Ok()
        {
            //Arrange
            var usuario = new UsuarioModel { UsuarioId = 1 };
            _usuarioServiceMock.Setup(service => service.GetUsuarioById(It.IsAny<int>())).Returns(new UsuarioModel());

            //Act
            var result = _usuarioController.GetUsuarioById(1);

            //Assert
            var actionResult = Assert.IsType<ActionResult<UsuarioViewModel>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void GetUsuarioById_Return_Not_Found()
        {
            //Arrange
            _usuarioServiceMock.Setup(service => service.GetUsuarioById(It.IsAny<int>())).Returns((UsuarioModel)null);

            //Act
            var result = _usuarioController.GetUsuarioById(1);

            //Assert
            var actionResult = Assert.IsType<ActionResult<UsuarioViewModel>>(result);
            var notFoundResult = Assert.IsType<NotFoundResult>(actionResult.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public void Create_ReturnCreatAtAction_Return_Ok()
        {
            //Arrange
            var usuarioModel = new UsuarioModel
            {
                UsuarioId = 1,
                Nome = "Usuário Teste",
                Email = "usuario@teste.com"
            };
            var usuarioViewModel = usuarioModel;

            _mapperMock.Setup(mapper => mapper.Map<UsuarioModel>(usuarioViewModel)).Returns(usuarioModel);
            _usuarioServiceMock.Setup(service => service.AddUsuario(usuarioModel));

            // Act
            var result = _usuarioController.Create(usuarioModel);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(201, createdResult.StatusCode);
            Assert.NotNull(createdResult.Value);
        }

        [Fact]
        public void Create_ReturnCreatAtAction_Bad_Request()
        {
            // Arrange
            var usuarioModel = new UsuarioModel();
            _mapperMock.Setup(mapper => mapper.Map<UsuarioModel>(usuarioModel)).Throws(new Exception("Erro inesperado"));

            // Act
            var result = _usuarioController.Create(usuarioModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("Erro inesperado", badRequestResult.Value);
        }

        [Fact]
        public void Update_Usuario_Return_Ok()
        {
            // Arrange
            var id = 1;
            var viewModel = new UsuarioViewModel { UsuarioId = id, Nome = "Usuário Teste" };
            var usuarioModel = new UsuarioModel { UsuarioId = id, Nome = "Usuário Teste" };

            _usuarioServiceMock.Setup(service => service.GetUsuarioById(id)).Returns(usuarioModel);
            _mapperMock.Setup(mapper => mapper.Map<UsuarioModel>(viewModel)).Returns(usuarioModel);
            _usuarioServiceMock.Setup(service => service.UpdateUsuario(usuarioModel));

            // Act
            var result = _usuarioController.UpdateUsuario(id, usuarioModel);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public void Update_Usuario_Bad_Request()
        {
            // Arrange
            var id = 1;
            var viewModel = new UsuarioModel { UsuarioId = 2, Nome = "Usuário Teste" };

            // Act
            var result = _usuarioController.UpdateUsuario(id, viewModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("O ID da rota não corresponde ao ID do objeto enviado.", badRequestResult.Value);
        }

        [Fact]
        public void Update_Usuario_Not_Found()
        {
            // Arrange
            var id = 1;
            var viewModel = new UsuarioModel { UsuarioId = id, Nome = "Usuário Teste" };

            _usuarioServiceMock.Setup(service => service.GetUsuarioById(id)).Returns((UsuarioModel)null);

            // Act
            var result = _usuarioController.UpdateUsuario(id, viewModel);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("Usuário não encontrado.", notFoundResult.Value);
        }

        [Fact]
        public void Detele_Usuario_Return_No_Content()
        {
            // Arrange
            var id = 1;
            _usuarioServiceMock.Setup(service => service.Delete(id)).Returns(true);

            // Act
            var result = _usuarioController.DeleteUsuario(id);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public void Delete_Usuario_Not_Found()
        {
            // Arrange
            var id = 1;
            _usuarioServiceMock.Setup(service => service.Delete(id)).Returns(false);

            // Act
            var result = _usuarioController.DeleteUsuario(id);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal($"Usuário com ID {id} não encontrado.", notFoundResult.Value);
        }
    }
}
