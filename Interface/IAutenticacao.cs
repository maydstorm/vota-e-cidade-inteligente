using VotaE_API.Models;
using VotaE_API.ViewModel.Usuario;

namespace VotaE_API.Interface
{
    public interface IAutenticacao
    {
        LoginViewModel Authenticate(string email, string senha);
    }
}
