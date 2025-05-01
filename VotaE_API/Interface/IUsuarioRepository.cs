using VotaE_API.Models;

namespace VotaE_API.Interface
{
    public interface IUsuarioRepository
    {
        IEnumerable<UsuarioModel> GetAll();
        UsuarioModel GetUsuarioById(int id);
        UsuarioModel GetByEmail(string email);
        void AddUsuario(UsuarioModel usuario);
        void UpdateUsuario(UsuarioModel usuario);
        bool DeleteUsuario(int id);
        int GetTotalUsuarios();
    }
}
