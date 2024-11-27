using Microsoft.EntityFrameworkCore;
using VotaE_API.Data;
using VotaE_API.Interface;
using VotaE_API.Models;

namespace VotaE_API.Repository
{
    public class ProjetoRepository : IProjetoRepository
    {
        private readonly DataBaseContext _dbContext;

        public ProjetoRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ProjetoModel> GetAll(int lastReference, int size)
        {
            var projetos = _dbContext.Projetos.Where(s => s.ProjetoId > lastReference)
               .OrderBy(s => s.ProjetoId)
               .Take(size)
               .AsNoTracking()
               .ToList();

            return projetos; ;
        }

        public ProjetoModel GetById(int id) => _dbContext.Projetos.Include(p => p.Sugestao).FirstOrDefault(p => p.ProjetoId == id);

        public void AddProjeto(ProjetoModel projeto)
        {
            _dbContext.Projetos.Add(projeto);
            _dbContext.SaveChanges();
        }

        public void UpdateProjeto(ProjetoModel projeto)
        {
            var projetoExistente = _dbContext.Projetos.Find(projeto.ProjetoId);

            _dbContext.Entry(projetoExistente).CurrentValues.SetValues(projeto);
            _dbContext.SaveChanges();
        }

        public bool DeleteProjeto(int id)
        {
            var projeto = _dbContext.Projetos.Find(id);

            if (projeto == null)
                return false;

            _dbContext.Projetos.Remove(projeto);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
