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

        public void AddUsuario(UsuarioModel usuario) => _repository.Add(usuario);

        public void UpdateUsuario(UsuarioModel usuario) => _repository.Update(usuario);

        public bool Delete(int id)
        {
            var usuario = _repository.GetById(id);
            if (usuario != null)
            {
                _repository.Delete(id); /* Julia - aqui repassei o ID não o obj inteiro */
                return true; /* Julia - se for encontrado */
            }
            return false; /* Julia - se não for encontrado */
        }

    }
}

