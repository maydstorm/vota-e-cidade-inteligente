using VotaE_API.Models;

namespace VotaE_API.Interface
{
    public interface IUsuarioRepository
    {
        IEnumerable<UsuarioModel> GetAll();
        UsuarioModel GetById(int Id);
        void Add(UsuarioModel usuario);
        void Update(UsuarioModel usuario);
        void Delete(UsuarioModel usuario);
    }
}
