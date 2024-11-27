using Microsoft.EntityFrameworkCore;
using VotaE_API.Data;
using VotaE_API.Interface;
using VotaE_API.Models;

namespace VotaE_API.Repository
{
    public class SugestaoRepository : ISugestaoRepository
    {
        private readonly DataBaseContext _dbContext;

        public SugestaoRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<SugestaoModel> GetAll(int lastReference, int size)
        {
            var sugestoes = _dbContext.Sugestoes.Where(s => s.SugestaoId > lastReference)
                .OrderBy(s => s.SugestaoId)
                .Take(size)
                .AsNoTracking()
                .ToList();

            return sugestoes;
        }

        public SugestaoModel GetSugestaoById(int id) => _dbContext.Sugestoes.Find(id);
        public void AddSugestao(SugestaoModel sugestao)
        {
            _dbContext.Sugestoes.Add(sugestao);
            _dbContext.SaveChanges();
        }

        public void UpdateSugestao(SugestaoModel sugestao)
        {
            var sugestaoExistente = _dbContext.Sugestoes.Find(sugestao.SugestaoId);

            _dbContext.Entry(sugestaoExistente).CurrentValues.SetValues(sugestao);
            _dbContext.SaveChanges();
        }

        public bool DeleteSugestao(int id)
        {
            var sugestao = _dbContext.Sugestoes.FirstOrDefault(s => s.SugestaoId == id);

            if (sugestao == null)
                return false;

            _dbContext.Sugestoes.Remove(sugestao);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
