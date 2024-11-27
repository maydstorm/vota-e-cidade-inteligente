using VotaE_API.Models;
using VotaE_API.ViewModel.Sugestao;

namespace VotaE_API.Interface
{
    public interface ISugestaoRepository
    {
        IEnumerable<SugestaoModel> GetAll(int lastReference, int size);
        SugestaoModel GetSugestaoById(int Id);
        void AddSugestao(SugestaoModel sugestao);
        void UpdateSugestao(SugestaoModel sugestao);
        bool DeleteSugestao(int id);
        int GetTotalSugestoes();
    }
}
