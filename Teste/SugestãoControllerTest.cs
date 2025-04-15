using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VotaE_API.Controllers;
using VotaE_API.Interface;
using VotaE_API.Models;
using VotaE_API.ViewModel.Sugestao;
using Xunit;

namespace Teste
{
    public class SugestãoControllerTest
    {
        private readonly Mock<ISugestaoService> _sugestaoServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly SugestaoController _sugestaoController;

        public SugestãoControllerTest()
        {
            _sugestaoServiceMock = new Mock<ISugestaoService>();
            _mapperMock = new Mock<IMapper>();
            _sugestaoController = new SugestaoController(_sugestaoServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void GetAllSugestoes_Return_Ok()
        {
            //Arrange
            var sugestoes = new List<SugestaoModel>
            {
            new SugestaoModel { SugestaoId = 1, Titulo = "Sugestao 1" },
            new SugestaoModel { SugestaoId = 2, Titulo = "Sugestao 2" }
            };

            _sugestaoServiceMock.Setup(service => service.GetAllSugestoes(It.IsAny<int>(), It.IsAny<int>())).Returns(sugestoes);

            var sugestoesViewModel = sugestoes.Select(s => new SugestaoViewModel { SugestaoId = s.SugestaoId, Titulo = s.Titulo });
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<SugestaoViewModel>>(It.IsAny<IEnumerable<SugestaoModel>>()))
                .Returns(sugestoesViewModel);

            // Act
            var result = _sugestaoController.GetAllSugestoes(0, 10);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var viewModel = Assert.IsType<SugestaoPaginacaoViewModel>(okResult.Value);

            Assert.Equal(2, viewModel.Sugestoes.Count());
            Assert.Equal(10, viewModel.PageSize);
            Assert.Equal(2, viewModel.NextRef);
        }

        [Fact]
        public void GetAllSugestoes_ReturnsNoContent()
        {
            // Arrange
            _sugestaoServiceMock.Setup(service => service.GetAllSugestoes(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<SugestaoModel>());

            // Act
            var result = _sugestaoController.GetAllSugestoes(0, 10);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public void GetSugestaoById_Return_Ok()
        {
            //Arrange
            var usuario = new UsuarioModel { UsuarioId = 1 };
            _sugestaoServiceMock.Setup(service => service.GetSugestaoById(It.IsAny<int>())).Returns(new SugestaoModel());

            //Act
            var result = _sugestaoController.GetSugestoById(1);

            //Assert
            var actionResult = Assert.IsType<ActionResult<SugestaoViewModel>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void GetUsuarioById_Return_Not_Found()
        {
            //Arrange
            _sugestaoServiceMock.Setup(service => service.GetSugestaoById(It.IsAny<int>())).Returns((SugestaoModel)null);

            //Act
            var result = _sugestaoController.GetSugestoById(1);

            //Assert
            var actionResult = Assert.IsType<ActionResult<SugestaoViewModel>>(result);
            var notFoundResult = Assert.IsType<NotFoundResult>(actionResult.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public void Create_ReturnCreatAtAction_Return_Ok()
        {
            //Arrange
            var sugestaoViewModel = new SugestaoViewModel
            {
                // Defina as propriedades do viewModel, por exemplo:
                Titulo = "Sugestão 1",
                Descricao = "Descrição da sugestão"
            };

            var sugestaoModel = new SugestaoModel
            {
                SugestaoId = 1,
                Titulo = "Sugestão 1",
                Descricao = "Descrição da sugestão"
            };

            _mapperMock.Setup(mapper => mapper.Map<SugestaoModel>(sugestaoViewModel)).Returns(sugestaoModel);
            _sugestaoServiceMock.Setup(service => service.AddSugestao(sugestaoModel));

            // Act
            var result = _sugestaoController.Create(sugestaoViewModel);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(201, createdResult.StatusCode);
            Assert.NotNull(createdResult.Value);
        }

        [Fact]
        public void Create_ReturnCreatAtAction_Bad_Request()
        {
            // Arrange
            var sugestaoViewModel = new SugestaoViewModel
            {
                // Defina as propriedades do viewModel
                Titulo = "Sugestão 1",
                Descricao = "Descrição da sugestão"
            };

            _sugestaoServiceMock.Setup(service => service.AddSugestao(It.IsAny<SugestaoModel>())).Throws(new Exception("Erro inesperado"));

            // Act
            var result = _sugestaoController.Create(sugestaoViewModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("Erro inesperado", badRequestResult.Value);
        }

        [Fact]
        public void Update_Sugestao_Return_Ok()
        {
            // Arrange
            var id = 1;
            var viewModel = new SugestaoViewModel { SugestaoId = id, Titulo = "Sugestão Teste" };
            var sugestaoModel = new SugestaoModel { SugestaoId = id, Titulo = "Sugestão Teste" };

            _sugestaoServiceMock.Setup(service => service.GetSugestaoById(id)).Returns(sugestaoModel);
            _mapperMock.Setup(mapper => mapper.Map<SugestaoModel>(viewModel)).Returns(sugestaoModel);
            _sugestaoServiceMock.Setup(service => service.UpdateSugestao(sugestaoModel));

            // Act
            var result = _sugestaoController.UpdateSugestao(id, viewModel);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public void Update_Sugestao_Bad_Request()
        {
            // Arrange
            var id = 1;
            var viewModel = new SugestaoViewModel { SugestaoId = 2, Titulo = "Sugestão Teste" };

            // Act
            var result = _sugestaoController.UpdateSugestao(id, viewModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("O ID da rota não corresponde ao ID do objeto enviado.", badRequestResult.Value);
        }

        [Fact]
        public void Update_Sugestao_Not_Found()
        {
            // Arrange
            var id = 1;
            var viewModel = new SugestaoViewModel { SugestaoId = id, Titulo = "Sugestão Teste" };

            _sugestaoServiceMock.Setup(service => service.GetSugestaoById(id)).Returns((SugestaoModel)null);

            // Act
            var result = _sugestaoController.UpdateSugestao(id, viewModel);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("Usuário não encontrado.", notFoundResult.Value);
        }

        [Fact]
        public void Detele_Sugestao_Return_No_Content()
        {
            // Arrange
            var id = 1;
            _sugestaoServiceMock.Setup(service => service.DeleteSugestao(id)).Returns(true);

            // Act
            var result = _sugestaoController.DeleteSugestao(id);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public void Delete_Sugestao_Not_Found()
        {
            // Arrange
            var id = 1;
            _sugestaoServiceMock.Setup(service => service.DeleteSugestao(id)).Returns(false);

            // Act
            var result = _sugestaoController.DeleteSugestao(id);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal($"Sugestão com ID {id} não encontrado.", notFoundResult.Value);
        }
    }
}
