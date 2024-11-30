using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VotaE_API.Interface;
using VotaE_API.Models;
using VotaE_API.ViewModel.Usuario;

namespace VotaE_API.Controllers
{
    [Route("api/usuario/")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;

        }

        [HttpGet]
        [Authorize(Roles = "adm")]
        public ActionResult<IEnumerable<UsuarioPaginacaoViewModel>> GetAllUsuarios([FromQuery] int reference = 0, int tamanho = 10)
        {
            var usuarios = _usuarioService.GetAllUsuarios(reference, tamanho);

            if (usuarios != null && usuarios.Any())
            {
                var viewModelList = _mapper.Map<IEnumerable<UsuarioViewModel>>(usuarios);
                var ViewModel = new UsuarioPaginacaoViewModel
                {
                    Usuarios = viewModelList,
                    PageSize = tamanho,
                    Ref = reference,
                    NextRef = (int)viewModelList.Last().UsuarioId
                };

                return Ok(ViewModel);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "adm")]
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
            try
            {
                var model = _mapper.Map<UsuarioModel>(viewModel);
                _usuarioService.AddUsuario(model);

                return CreatedAtAction(nameof(GetUsuarioById), new { id = model.UsuarioId }, model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public ActionResult UpdateUsuario([FromRoute] int id, [FromBody] UsuarioViewModel viewModel)
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
            catch (Exception)
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

        [HttpGet("total")]
        [Authorize(Roles = "adm")]
        public ActionResult TotalUsuarios()
        {
            var totalUsuarios = _usuarioService.TotalUsuarios();

            if(totalUsuarios == 0)
            {
                return NoContent();
            }

            return Ok(new { totalUsuarios });
        }
    }
}
