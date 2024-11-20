using VotaE_API.Models;

namespace VotaE_API.Interface
{
    public interface IUsuarioService
    {
        IEnumerable<UsuarioModel> GetAllUsuarios();
        UsuarioModel GetUsuarioById(int Id);
        void AddUsuario(UsuarioModel usuario);
        void UpdateUsuario(UsuarioModel usuario);
        bool Delete(int id);
    }
}
