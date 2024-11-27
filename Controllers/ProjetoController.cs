using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VotaE_API.Interface;
using VotaE_API.Models;
using VotaE_API.ViewModel.Projeto;

namespace VotaE_API.Controllers
{
    [Route("api/projeto")]
    [ApiController]
    public class ProjetoController : ControllerBase
    {
        private readonly IProjetoService _projetoService;
        private readonly IMapper _mapper;

        public ProjetoController(IProjetoService projetoService, IMapper mapper)
        {
            _projetoService = projetoService;
            _mapper = mapper;

        }

        [HttpGet]
        public ActionResult<IEnumerable<ProjetoViewModel>> GetAllProjetos()
        {
            var projetos = _projetoService.GetAllProjetos();

            if (projetos != null && projetos.Any())
            {
                var viewModelList = _mapper.Map<IEnumerable<ProjetoViewModel>>(projetos);

                return Ok(viewModelList);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ProjetoViewModel> GetProjetoById([FromRoute] int id)
        {
            var projeto = _projetoService.GetProjetoById(id);

            if (projeto != null)
            {
                var viewModel = _mapper.Map<ProjetoViewModel>(projeto);

                return Ok(viewModel);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        public ActionResult Create([FromBody] ProjetoViewModel projetoViewModel)
        {
            try
            {
                var model = _mapper.Map<ProjetoModel>(projetoViewModel);
                _projetoService.AddProjeto(model);

                return CreatedAtAction(nameof(GetProjetoById), new { id = model.ProjetoId }, model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public ActionResult UpdateProjeto([FromRoute] int id, [FromBody] ProjetoViewModel projetoViewModel)
        {
            if (id != projetoViewModel.ProjetoId)
            {
                return BadRequest("O ID da rota não corresponde ao ID do projeto enviado.");
            }

            try
            {
                var projetoExistente = _projetoService.GetProjetoById(id);
                if (projetoExistente == null)
                {
                    return NotFound("Projeto não encontrado.");
                }

                var model = _mapper.Map<ProjetoModel>(projetoViewModel);
                _projetoService.UpdateProjeto(model);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest("Erro interno do servidor ao atualizar o projeto.");
            }
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteProjeto(int id)
        {
            var result = _projetoService.Delete(id);
            if (!result)
                return NotFound($"Projeto com ID {id} não encontrado.");

            return NoContent();
        }

        [HttpGet("porcentagem/sugestoes/projetos")]
        public ActionResult<object> GetPorcentagemSugestoesProjetos()
        {
            var porcentagem = _projetoService.GetPorcentagemSugestoesProjetos();
            return Ok(new { porcentagem });
        }


    }
}