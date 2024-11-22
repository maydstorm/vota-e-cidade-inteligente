using VotaE_API.Models;

namespace VotaE_API.Interface
{
    public interface IAutenticacao
    {
        UsuarioModel Authenticate(string nome, string senha);
    }
}
