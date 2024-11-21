using VotaE_API.Models;

namespace VotaE_API.Interface
{
    public interface ISugestaoService
    {
        IEnumerable<SugestaoModel> GetAllSugestoes();
        SugestaoModel GetSugestaoById(int Id);
        void AddSugestao(SugestaoModel sugestao);
        void UpdateSugestao(SugestaoModel sugestao);
        bool DeleteSugestao(int id);
    }
}
