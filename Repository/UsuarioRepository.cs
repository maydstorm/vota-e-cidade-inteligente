using VotaE_API.Data;
using VotaE_API.Interface;
using VotaE_API.Models;

namespace VotaE_API.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataBaseContext _dbContext;

        public UsuarioRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<UsuarioModel> GetAll()
        {
            return _dbContext.Usuarios.ToList();
        }

        public UsuarioModel GetById(int id) => _dbContext.Usuarios.Find(id);
        public void Add(UsuarioModel usuario)
        {
            _dbContext.Usuarios.Add(usuario);
            _dbContext.SaveChanges();
        }

        public void Update(UsuarioModel usuario)
        {
            _dbContext.Usuarios.Update(usuario);
            _dbContext.SaveChanges();
        }

        public void Delete(UsuarioModel usuario)
        {
            _dbContext.Usuarios.Remove(usuario);
            _dbContext.SaveChanges();
        }

    }
}
