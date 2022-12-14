using System.Linq.Expressions;
using webNET_Hits_backend_aspnet_project_2.Services;

namespace webNET_Hits_backend_aspnet_project_2.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(Guid id);
        List<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        Task Remove(T entity);
        Task RemoveRange(IEnumerable<T> entities);
        Task Update(T entity);
    }
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataBaseContext _context;

        public GenericRepository(DataBaseContext context)
        {
            _context = context;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public T GetById(Guid id)
        {
            return _context.Set<T>().Find(id);
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }
        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task AddRange(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
        public async Task Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
