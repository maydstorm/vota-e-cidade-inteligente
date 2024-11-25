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

        public IEnumerable<ProjetoModel> GetAll()
        {
            return _dbContext.Projetos.Include(p => p.Sugestao).ToList();
        }

        public ProjetoModel GetById(int id)
        {
            return _dbContext.Projetos.Include(p => p.Sugestao).FirstOrDefault(p => p.ProjetoId == id);
        }

        public void Add(ProjetoModel projeto)
        {
            _dbContext.Projetos.Add(projeto);
            _dbContext.SaveChanges();
        }

        public void Update(ProjetoModel projeto)
        {
            _dbContext.Projetos.Update(projeto);
            _dbContext.SaveChanges();
        }

        public bool Delete(int id)
        {
            var projeto = _dbContext.Projetos.FirstOrDefault(p => p.ProjetoId == id);
            if (projeto == null)
                return false;

            _dbContext.Projetos.Remove(projeto);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
