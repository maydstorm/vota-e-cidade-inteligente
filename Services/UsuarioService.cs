using VotaE_API.Interface;
using VotaE_API.Models;

namespace VotaE_API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<UsuarioModel> GetAllUsuarios() => _repository.GetAll();

        public UsuarioModel GetUsuarioById(int id) => _repository.GetById(id);

        public void AddUsuario(UsuarioModel usuario) => _repository.AddUsuario(usuario);

        public void UpdateUsuario(UsuarioModel usuario) => _repository.UpdateUsuario(usuario);

        public bool Delete(int id)
        {
            var usuario = _repository.GetById(id);
            if (usuario != null)
            {
                _repository.DeleteUsuario(id); 
                return true; 
            }
            return false; 
        }
    }
}

