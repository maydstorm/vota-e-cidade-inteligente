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

        public IEnumerable<SugestaoModel> GetAllSugestoes(int lastReference, int size)
        {
            return _repository.GetAll(lastReference, size);
        } 

        public SugestaoModel GetSugestaoById(int id) => _repository.GetSugestaoById(id);

        public void AddSugestao(SugestaoModel sugestao)
        {
            // Verifica se o usuário existe
            var usuario = _usuarioService.GetUsuarioById(sugestao.UsuarioId);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            // Preenche a data de criação
            sugestao.DataCriacao = DateTime.UtcNow;

            // Associa o usuário à sugestão (se necessário)
            sugestao.Usuario = usuario;

            // Adiciona a sugestão no repositório
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
