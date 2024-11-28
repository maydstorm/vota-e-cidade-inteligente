using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<UsuarioModel> GetAll(int lastReference, int size)
        {
            var usuarios = _dbContext.Usuarios.Where( u => u.UsuarioId > lastReference)
                .OrderBy( u => u.UsuarioId)
                .Take(size)
                .AsNoTracking()
                .ToList();

            return usuarios;
        }

        public UsuarioModel GetById(int id) => _dbContext.Usuarios.Find(id);

        public UsuarioModel GetByEmail(string email)
        {
            return _dbContext.Usuarios.FirstOrDefault(u => u.Email == email); 
        }

        public void AddUsuario(UsuarioModel usuario)
        {
            _dbContext.Usuarios.Add(usuario);
            _dbContext.SaveChanges();
        }

        public void UpdateUsuario(UsuarioModel usuario)
        {
            var usuarioExistente = _dbContext.Usuarios.Find(usuario.UsuarioId);

            _dbContext.Entry(usuarioExistente).CurrentValues.SetValues(usuario);
            _dbContext.SaveChanges();
        }

        public bool DeleteUsuario(int id)
        {
            var usuario = _dbContext.Usuarios.FirstOrDefault(u => u.UsuarioId == id);

            if (usuario == null)
                return false; 

            _dbContext.Usuarios.Remove(usuario);
            _dbContext.SaveChanges();

            return true;
        }

        public int GetTotalUsuarios()
        {
            return _dbContext.Usuarios.Count();
        }

    }
}
