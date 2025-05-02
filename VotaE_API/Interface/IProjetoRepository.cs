using VotaE_API.Models;

namespace VotaE_API.Interface
{
    public interface IProjetoRepository
    {
        IEnumerable<ProjetoModel> GetAll();
        ProjetoModel GetById(int id);
        void AddProjeto(ProjetoModel projeto);
        void UpdateProjeto(ProjetoModel projeto);
        bool DeleteProjeto(int id);
        int GetTotalProjetos();

    }
}
