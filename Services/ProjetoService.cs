using VotaE_API.Interface;
using VotaE_API.Models;

namespace VotaE_API.Services
{
    public class ProjetoService : IProjetoService
    {
        private readonly IProjetoRepository _repository;

        public ProjetoService(IProjetoRepository repository)
        {
            _repository = repository;
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
    }
}
