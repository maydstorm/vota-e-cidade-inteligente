using VotaE_API.Interface;
using VotaE_API.Models;

namespace VotaE_API.Services
{
    public class SugestaoService : ISugestaoService
    {
        private readonly ISugestaoRepository _repository;

        public SugestaoService(ISugestaoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<SugestaoModel> GetAllSugestoes() => _repository.GetAll();

        public SugestaoModel GetSugestaoById(int id) => _repository.GetSugestaoById(id);

        public void AddSugestao(SugestaoModel sugestao) => _repository.AddSugestao(sugestao);

        public void UpdateSugestao(SugestaoModel sugestao) => _repository.UpdateSugestao(sugestao);

        public bool DeleteSugestao(int id)
        {
            var sugestao = _repository.GetSugestaoById(id);
            if (sugestao != null)
            {
                _repository.DeleteSugestao(id);
                return true;
            }
            return false;
        }
    }
}
