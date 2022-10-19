using currus.Data;
using Microsoft.EntityFrameworkCore;

namespace currus.Repository
{
    public class DbRepository<T> : IDbRepository<T> where T : class
    {
        private ApplicationDbContext _context;
        public DbRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(T entity)
        {
            await _context.AddAsync(entity);
        }
        
        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T? Get(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
