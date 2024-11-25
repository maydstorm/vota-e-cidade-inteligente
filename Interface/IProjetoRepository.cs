using VotaE_API.Models;

namespace VotaE_API.Interface
{
    public interface IProjetoRepository
    {
        IEnumerable<ProjetoModel> GetAll();
        ProjetoModel GetById(int id);
        void Add(ProjetoModel projeto);
        void Update(ProjetoModel projeto);
        bool Delete(int id);
    }
}
