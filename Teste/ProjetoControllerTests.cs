using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VotaE_API.Controllers;
using VotaE_API.Interface;
using VotaE_API.Models;
using VotaE_API.ViewModel.Projeto;
using Xunit;

namespace Teste
{
    public class ProjetoControllerTest
    {
        private readonly Mock<IProjetoService> _projetoServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProjetoController _projetoController;

        public ProjetoControllerTest()
        {
            _projetoServiceMock = new Mock<IProjetoService>();
            _mapperMock = new Mock<IMapper>();
            _projetoController = new ProjetoController(_projetoServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void GetAllProjetos_Return_Ok()
        {
            // Arrange
            var projetos = new List<ProjetoModel>
            {
                new ProjetoModel { ProjetoId = 1, Titulo = "Projeto 1"},
                new ProjetoModel { ProjetoId = 2, Titulo = "Projeto 2"}
            };

            _projetoServiceMock.Setup(service => service.GetAllProjetos(It.IsAny<int>(), It.IsAny<int>())).Returns(projetos);

            var projetosViewModel = projetos.Select(p => new ProjetoViewModel { ProjetoId = p.ProjetoId, Titulo = p.Titulo, Descricao = p.Descricao });
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ProjetoViewModel>>(It.IsAny<IEnumerable<ProjetoModel>>()))
                .Returns(projetosViewModel);

            // Act
            var result = _projetoController.GetAllProjetos(0, 10);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var viewModel = Assert.IsType<ProjetoPaginacaoViewModel>(okResult.Value);

            Assert.Equal(2, viewModel.Projetos.Count());
            Assert.Equal(10, viewModel.PageSize);
            Assert.Equal(2, viewModel.NextRef);
        }

        [Fact]
        public void GetAllProjetos_Return_NoContent()
        {
            // Arrange
            _projetoServiceMock.Setup(service => service.GetAllProjetos(It.IsAny<int>(), It.IsAny<int>())).Returns(new List<ProjetoModel>());

            // Act
            var result = _projetoController.GetAllProjetos(0, 10);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public void GetProjetoById_Return_Ok()
        {
            // Arrange
            var projeto = new ProjetoModel { ProjetoId = 1};
            _projetoServiceMock.Setup(service => service.GetProjetoById(It.IsAny<int>())).Returns(new ProjetoModel());

            //Act
            var result = _projetoController.GetProjetoById(1);

            //Assert
            var actionResult = Assert.IsType<ActionResult<ProjetoViewModel>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void GetProjetoById_Return_NotFound()
        {
            // Arrange
            _projetoServiceMock.Setup(service => service.GetProjetoById(It.IsAny<int>())).Returns((ProjetoModel)null);

            // Act
            var result = _projetoController.GetProjetoById(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProjetoViewModel>>(result);
            var notFoundResult = Assert.IsType<NotFoundResult>(actionResult.Result);

            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public void CreateProjeto_ReturnCreatedAtAction_Return_Ok()
        {
            // Arrange
            var projetoViewModel = new ProjetoViewModel 
            { 
                Titulo = "Novo Projeto", 
                Descricao = "Descrição do projeto"
            };

            var projetoModel = new ProjetoModel 
            { 
                ProjetoId = 1, 
                Titulo = "Novo Projeto", 
                Descricao = "Descrição do projeto" };

            _mapperMock.Setup(mapper => mapper.Map<ProjetoModel>(projetoViewModel)).Returns(projetoModel);
            _projetoServiceMock.Setup(service => service.AddProjeto(projetoModel));

            // Act
            var result = _projetoController.Create(projetoViewModel);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, createdResult.StatusCode);
            Assert.NotNull(createdResult.Value);
        }

        [Fact]
        public void CreateProjeto_Return_BadRequest()
        {
            // Arrange
            var projetoViewModel = new ProjetoViewModel 
            { 
                Titulo = "Novo Projeto", 
                Descricao = "Descrição do projeto" 
            };

            _projetoServiceMock.Setup(service => service.AddProjeto(It.IsAny<ProjetoModel>())).Throws(new Exception("Erro inesperado"));

            // Act
            var result = _projetoController.Create(projetoViewModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("Erro inesperado", badRequestResult.Value);
        }

        [Fact]
        public void Update_Projeto_Return_Ok()
        {
            // Arrange
            var id = 1;
            var viewModel = new ProjetoViewModel { ProjetoId = id, Titulo = "Projeto Teste" };
            var projetoModel = new ProjetoModel { ProjetoId = id, Titulo = "Projeto Teste" };

            _projetoServiceMock.Setup(service => service.GetProjetoById(id)).Returns(projetoModel);
            _mapperMock.Setup(mapper => mapper.Map<ProjetoModel>(viewModel)).Returns(projetoModel);
            _projetoServiceMock.Setup(service => service.UpdateProjeto(projetoModel));

            // Act
            var result = _projetoController.UpdateProjeto(id, viewModel);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public void Update_Projeto_Bad_Request()
        {
            // Arrange
            var id = 1;
            var viewModel = new ProjetoViewModel { ProjetoId = 2, Titulo = "Projeto Teste" };

            // Act
            var result = _projetoController.UpdateProjeto(id, viewModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("O ID da rota não corresponde ao ID do projeto enviado.", badRequestResult.Value);
        }

        [Fact]
        public void Update_Projeto_Not_Found()
        {
            // Arrange
            var id = 1;
            var viewModel = new ProjetoViewModel { ProjetoId = id, Titulo = "Sugestão Teste" };

            _projetoServiceMock.Setup(service => service.GetProjetoById(id)).Returns((ProjetoModel)null);

            // Act
            var result = _projetoController.UpdateProjeto(id, viewModel);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("Projeto não encontrado.", notFoundResult.Value);
        }

        [Fact]
        public void DeleteProjeto_Return_NoContent()
        {
            // Arrange
            var projetoId = 1;
            _projetoServiceMock.Setup(service => service.Delete(It.IsAny<int>())).Returns(true);

            // Act
            var result = _projetoController.DeleteProjeto(projetoId);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public void DeleteProjeto_Return_NotFound()
        {
            // Arrange
            var projetoId = 1;
            _projetoServiceMock.Setup(service => service.Delete(It.IsAny<int>())).Returns(false);

            // Act
            var result = _projetoController.DeleteProjeto(projetoId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal($"Projeto com ID {projetoId} não encontrado.", notFoundResult.Value);
        }

        [Fact]
        public void GetProjetoMaisVotado_Return_Ok()
        {
            // Arrange
            var projetoMaisVotado = new ProjetoModel { ProjetoId = 1, Titulo = "Projeto Mais Votado", Votos = 100 };
            _projetoServiceMock.Setup(service => service.GetProjetoMaisVotado()).Returns(projetoMaisVotado);

            var projetoViewModel = new ProjetoViewModel { ProjetoId = 1, Titulo = "Projeto Mais Votado", Votos = 100 };
            _mapperMock.Setup(mapper => mapper.Map<ProjetoViewModel>(projetoMaisVotado)).Returns(projetoViewModel);

            // Act
            var result = _projetoController.GetProjetoMaisVotado();

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProjetoViewModel>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var viewModel = Assert.IsType<ProjetoViewModel>(okResult.Value);

            Assert.Equal(1, viewModel.ProjetoId);
            Assert.Equal("Projeto Mais Votado", viewModel.Titulo);
        }
    }
}
