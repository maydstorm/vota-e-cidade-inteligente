using VotaE_API.Interface;
using VotaE_API.Models;

namespace VotaE_API.Services
{
    public class ProjetoService : IProjetoService
    {
        private readonly IProjetoRepository _repository;
        private readonly ISugestaoRepository _sugestaoRepository;
        private readonly IProjetoRepository _projetoRepository;

        public ProjetoService(IProjetoRepository repository, ISugestaoRepository sugestaoRepository, IProjetoRepository projetoRepository)
        {
            _repository = repository;
            _sugestaoRepository = sugestaoRepository;
            _projetoRepository = projetoRepository;
        }

        public IEnumerable<ProjetoModel> GetAllProjetos(int lastReference, int size)
        {
            return _repository.GetAll(lastReference, size);
        }

        public ProjetoModel GetProjetoById(int id) => _repository.GetById(id);

        public void AddProjeto(ProjetoModel projeto)
        {
            _repository.AddProjeto(projeto);
        }

        public void UpdateProjeto(ProjetoModel projeto)
        {
            var projetoExistente = _repository.GetById(projeto.ProjetoId);

            if (projetoExistente == null)
                throw new KeyNotFoundException("Projeto não encontrado.");

            _repository.UpdateProjeto(projeto);
        }

        public bool Delete(int id)
        {
            var projeto = _repository.GetById(id);
            if (projeto != null)
            {
                _repository.DeleteProjeto(id);
                return true;
            }
            return false;
        }

        public decimal GetPorcentagemSugestoesProjetos()
        {
            var totalSugestoes = _sugestaoRepository.GetTotalSugestoes();
            var totalProjetos = _projetoRepository.GetTotalProjetos();

            if (totalSugestoes == 0)
                return 0;

            return ((decimal)totalProjetos / totalSugestoes) * 100;
        }
        public void AddVotoProjeto(ProjetoModel projeto)
        {

            projeto.Votos = new Random().Next(1, 201);

            _repository.AddProjeto(projeto);
        }

    }
}