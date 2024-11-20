using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VotaE_API.Interface;
using VotaE_API.Models;
using VotaE_API.ViewModel.Usuario;

namespace VotaE_API.Controllers
{
    [Route("api/[controller]")]
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

            if (usuarios != null)
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
                var viewModel = _mapper.Map<IEnumerable<UsuarioModel>>(usuario);

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

            return CreatedAtAction(nameof(Create), new { id = model.UsuarioId }, model);
        }

        [HttpPost("{id}")]
        public ActionResult UpdateUsuario([FromRoute] int id, [FromBody] UsuarioModel viewModel)
        {

            if (viewModel.UsuarioId == id) //verificar 
            {
                var model = _mapper.Map<UsuarioModel>(viewModel);
                _usuarioService.UpdateUsuario(model);

                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUsuario([FromRoute] int id)
        {
            _usuarioService.Delete(id);

            return NoContent();
        }
    }
}
