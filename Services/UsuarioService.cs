using Microsoft.AspNetCore.Identity;
using VotaE_API.Interface;
using VotaE_API.Models;

namespace VotaE_API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly PasswordHasher<UsuarioModel> _passwordHasher;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
            _passwordHasher = new PasswordHasher<UsuarioModel>();
        }

        public IEnumerable<UsuarioModel> GetAllUsuarios() => _repository.GetAll().OrderBy(u => u.UsuarioId);

        public UsuarioModel GetUsuarioById(int id) => _repository.GetById(id);

        public UsuarioModel GetByEmail (string email) => _repository.GetByEmail(email);

        public void AddUsuario(UsuarioModel usuario)
        {
            usuario.Senha = _passwordHasher.HashPassword(usuario, usuario.Senha);
            _repository.AddUsuario(usuario);
        }

        public void UpdateUsuario(UsuarioModel usuario) 
        {
            var usuarioExiste = _repository.GetById(usuario.UsuarioId);

            if (usuarioExiste == null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            if (usuarioExiste.Senha != usuario.Senha)
            {
                usuario.Senha = _passwordHasher.HashPassword(usuario, usuario.Senha);
            }

            _repository.UpdateUsuario(usuario);
        }

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

