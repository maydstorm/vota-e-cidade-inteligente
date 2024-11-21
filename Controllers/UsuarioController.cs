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
        public ActionResult<IEnumerable<UsuarioModel>> GetAllUsuarios()
        {
            var usuarios = _usuarioService.GetAllUsuarios();

            if (usuarios != null && usuarios.Any())
            {
                var viewModelList = _mapper.Map<IEnumerable<UsuarioModel>>(usuarios);

                return Ok(viewModelList);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<UsuarioModel> GetUsuarioById([FromRoute] int id)
        {
            var usuario = _usuarioService.GetUsuarioById(id);

            if (usuario != null)
            {
                var viewModel = _mapper.Map<UsuarioModel>(usuario);

                return Ok(viewModel);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        public ActionResult Create([FromBody] UsuarioViewModel viewModel)
        {
            var model = _mapper.Map<UsuarioModel>(viewModel);
            _usuarioService.AddUsuario(model);

            return CreatedAtAction(nameof(GetUsuarioById), new { id = model.UsuarioId }, model);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUsuario([FromRoute] int id, [FromBody] UsuarioViewModel viewModel)
        {
            if (viewModel.UsuarioId == id) 
            {
                var model = _mapper.Map<UsuarioModel>(viewModel);
                _usuarioService.UpdateUsuario(model);

                return NoContent();
            }
            else
            {
                return BadRequest("O ID da rota não corresponde ao ID do objeto enviado.");
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
