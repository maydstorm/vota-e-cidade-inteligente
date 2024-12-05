using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VotaE_API.Interface;
using VotaE_API.Models;
using VotaE_API.ViewModel.Sugestao;

namespace VotaE_API.Controllers
{
    [Route("api/sugestao/")]
    [ApiController]
    [Authorize]
    public class SugestaoController : ControllerBase
    {
        private readonly ISugestaoService _sugestaoService;
        private readonly IMapper _mapper;

        public SugestaoController(ISugestaoService sugestaoService, IMapper mapper)
        {
            _sugestaoService = sugestaoService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SugestaoPaginacaoViewModel>> GetAllSugestoes([FromQuery] int reference = 0, int tamanho = 10)
        {
            var sugestao = _sugestaoService.GetAllSugestoes(reference, tamanho);

            if(sugestao != null && sugestao.Any())
            {
                var viewModelList = _mapper.Map<IEnumerable<SugestaoViewModel>>(sugestao);
                var ViewModel = new SugestaoPaginacaoViewModel
                {
                    Sugestoes = viewModelList,
                    PageSize = tamanho,
                    Ref = reference,
                    NextRef = (int)viewModelList.Last().SugestaoId
                };

                return Ok(ViewModel);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<SugestaoViewModel> GetSugestoById([FromRoute] int id)
        {
            var sugestao = _sugestaoService.GetSugestaoById(id);

            if (sugestao != null)
            {
                var viewModel = _mapper.Map<SugestaoViewModel>(sugestao);

                return Ok(viewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] SugestaoViewModel viewModel) 
        {
            try
            {
                var model = _mapper.Map<SugestaoModel>(viewModel);
                _sugestaoService.AddSugestao(model);

                return CreatedAtAction(nameof(GetSugestoById), new { id = model.SugestaoId }, model); 
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateSugestao([FromRoute] int id, [FromBody] SugestaoViewModel viewModel)
        {
            if (id != viewModel.SugestaoId)
            {
                return BadRequest("O ID da rota não corresponde ao ID do objeto enviado.");
            }

            try
            {
                var sugestaoExistente = _sugestaoService.GetSugestaoById(id);

                if (sugestaoExistente == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                var model = _mapper.Map<SugestaoModel>(viewModel);
                _sugestaoService.UpdateSugestao(model);

                return NoContent(); 
            }
            catch (Exception)
            {
                return BadRequest("Erro interno do servidor ao atualizar o usuário.");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteSugestao(int id)
        {
            var result = _sugestaoService.DeleteSugestao(id);

            if (!result)
                return NotFound($"Sugestão com ID {id} não encontrado.");

            return NoContent();
        }

        [HttpGet("total")]
        [Authorize(Roles ="adm")]
        public ActionResult GetTotalSugestoes()
        {
            var totalSugestao = _sugestaoService.GetSugestaoCount();

            if (totalSugestao == 0)
            {
                return NotFound("Sem sugestões cadastrados");
            }

            return Ok(new { totalSugestao });
        }
    }
}
