using VotaE_API.Interface;
using VotaE_API.Models;

namespace VotaE_API.Services
{
    public class SugestaoService : ISugestaoService
    {
        private readonly ISugestaoRepository _repository;
        private readonly IUsuarioService _usuarioService;

        public SugestaoService(ISugestaoRepository repository, IUsuarioService usuarioService)
        {
            _repository = repository;
            _usuarioService = usuarioService;
        }

        public IEnumerable<SugestaoModel> GetAllSugestoes() => _repository.GetAll().OrderBy(s => s.SugestaoId);

        public SugestaoModel GetSugestaoById(int id) => _repository.GetSugestaoById(id);

        public void AddSugestao(SugestaoModel sugestao)
        {
            if (!_usuarioService.Equals(sugestao.UsuarioId))
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            sugestao.DataCriacao = DateTime.UtcNow;
            _repository.AddSugestao(sugestao);
        }  

        public void UpdateSugestao(SugestaoModel sugestao)
        {
            var sugestaoExistente = _repository.GetSugestaoById(sugestao.SugestaoId);

            if (sugestaoExistente == null)
                throw new KeyNotFoundException("Sugestão não encontrado.");

            sugestao.DataCriacao = DateTime.UtcNow;
            _repository.UpdateSugestao(sugestao);
        }

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
