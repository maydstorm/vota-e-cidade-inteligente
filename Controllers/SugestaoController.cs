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
        public ActionResult<IEnumerable<SugestaoModel>> GetAllSugestoes()
        {
            var sugestao = _sugestaoService.GetAllSugestoes();

            if(sugestao != null && sugestao.Any())
            {
                var viewModelList = _mapper.Map<IEnumerable<SugestaoModel>>(sugestao);

                return Ok(viewModelList);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<SugestaoModel> GetSugestoById([FromRoute] int id)
        {
            var sugestao = _sugestaoService.GetSugestaoById(id);

            if (sugestao != null)
            {
                var viewModel = _mapper.Map<SugestaoModel>(sugestao);

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
            var model = _mapper.Map<SugestaoModel>(viewModel);
            _sugestaoService.AddSugestao(model);

            return CreatedAtAction(nameof(GetSugestoById), new { id = model.SugestaoId }, model); ;
        }

        [HttpPut]
        public ActionResult<SugestaoModel> UpdateSugestao([FromRoute] int id, [FromBody] SugestaoViewModel viewModel)
        {
            if (viewModel.SugestaoId == id)
            {
                var model = _mapper.Map<SugestaoModel>(viewModel);
                _sugestaoService.UpdateSugestao(model);

                return NoContent();
            }
            else
            {
                return BadRequest("O ID da rota não corresponde ao ID do objeto enviado.");
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
