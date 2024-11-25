using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VotaE_API.Interface;
using VotaE_API.Models;
using VotaE_API.ViewModel.Usuario;

namespace VotaE_API.Controllers
{
    [Route("api/usuario/")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;

        }

        [HttpGet]
        public ActionResult GetAllUsuarios()
        {
            var usuarios = _usuarioService.GetAllUsuarios();

            if (usuarios != null && usuarios.Any())
            {
                var viewModelList = _mapper.Map<IEnumerable<UsuarioViewModel>>(usuarios);

                return Ok(viewModelList);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<UsuarioViewModel> GetUsuarioById([FromRoute] int id)
        {
            var usuario = _usuarioService.GetUsuarioById(id);

            if (usuario != null)
            {
                var viewModel = _mapper.Map<UsuarioViewModel>(usuario);

                return Ok(viewModel);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        public ActionResult Create([FromBody] UsuarioModel viewModel)
        {
            var model = _mapper.Map<UsuarioModel>(viewModel);
            _usuarioService.AddUsuario(model);

            return CreatedAtAction(nameof(GetUsuarioById), new { id = model.UsuarioId }, model);
        }
    
        [HttpPut("{id}")]
        public ActionResult UpdateUsuario([FromRoute] int id, [FromBody] UsuarioModel viewModel)
        {
            if (id != viewModel.UsuarioId)
            {
                return BadRequest("O ID da rota não corresponde ao ID do objeto enviado.");
            }

            try
            {
                var usuarioExistente = _usuarioService.GetUsuarioById(id);
                if (usuarioExistente == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                var model = _mapper.Map<UsuarioModel>(viewModel);
                _usuarioService.UpdateUsuario(model);

                return NoContent(); 
            }
            catch (Exception ex)
            {
                return BadRequest("Erro interno do servidor ao atualizar o usuário.");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUsuario([FromRoute] int id)
        {
            var result = _usuarioService.Delete(id);

            if (!result)
                return NotFound($"Usuário com ID {id} não encontrado.");

            return NoContent();
        }
    }
}
