using Microsoft.AspNetCore.Identity;
using VotaE_API.Interface;
using VotaE_API.Models;

namespace VotaE_API.Services
{
    public class AutenticacaoService : IAutenticacao
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly PasswordHasher <UsuarioModel> _passwordHasher;

        public AutenticacaoService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository; 
            _passwordHasher = new PasswordHasher<UsuarioModel>();
        }

        public UsuarioModel Authenticate (string email, string senha)
        {
            var usuario = _usuarioRepository.GetByEmail(email);

            if (usuario == null)
            {
                return null;
            }

            var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.Senha, senha);
            if (result == PasswordVerificationResult.Failed)
            {
                return null; 
            }

            return usuario;
        }
    }
}
