using VotaE_API.Models;

namespace VotaE_API.Interface
{
    public interface IProjetoService
    {
        IEnumerable<ProjetoModel> GetAllProjetos(int lastReference, int size);
        ProjetoModel GetProjetoById(int id);
        void AddProjeto(ProjetoModel projeto);
        void UpdateProjeto(ProjetoModel projeto);
        bool Delete(int id);
    }
}
