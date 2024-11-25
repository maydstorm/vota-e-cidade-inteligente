using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VotaE_API.Interface;
using VotaE_API.Models;
using VotaE_API.ViewModel.Sugestao;

namespace VotaE_API.Controllers
{
    [Route("api/sugestao/")]
    [ApiController]
    public class SugestaoController : Controller
    {
        private readonly ISugestaoService _sugestaoService;
        private readonly IMapper _mapper;

        public SugestaoController(ISugestaoService sugestaoService, IMapper mapper)
        {
            _sugestaoService = sugestaoService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SugestaoViewModel>> GetAllSugestoes()
        {
            var sugestao = _sugestaoService.GetAllSugestoes();

            if(sugestao != null && sugestao.Any())
            {
                var viewModelList = _mapper.Map<IEnumerable<SugestaoViewModel>>(sugestao);

                return Ok(viewModelList);
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
        public ActionResult<SugestaoModel> Create([FromBody] SugestaoViewModel viewModel) 
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
        public ActionResult<SugestaoModel> UpdateSugestao([FromRoute] int id, [FromBody] SugestaoViewModel viewModel)
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
            catch (Exception ex)
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
    }
}
