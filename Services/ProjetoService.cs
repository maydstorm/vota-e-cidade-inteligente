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

        public IEnumerable<ProjetoModel> GetAllProjetos()
        {
            return _repository.GetAll();
        }

        public ProjetoModel GetProjetoById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddProjeto(ProjetoModel projeto)
        {
            _repository.Add(projeto);
        }

        public void UpdateProjeto(ProjetoModel projeto)
        {
            _repository.Update(projeto);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
