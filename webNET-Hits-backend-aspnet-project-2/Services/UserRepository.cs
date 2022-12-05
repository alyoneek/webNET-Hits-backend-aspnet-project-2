using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;

namespace webNET_Hits_backend_aspnet_project_2.Services
{
    public class UserRepository<T> : IEfRepository<T> where T : class //BaseEntity
    {
        private readonly DataBaseContext _context;

        public UserRepository(DataBaseContext context)
        {
            _context = context;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(Guid id)
        {
            //var result = _context.Set<T>().FirstOrDefault(x => x.Id == id);
            var result = _context.Set<T>().Find(id);
            if (result == null)
            {
                //todo: need to add logger
                return null;
            }

            return result;
        }

        public async Task<T> Add(T entity)
        {
            var result = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<T> Edit(T newEntity)
        {
            var result = _context.Set<T>().Update(newEntity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<T> Delete(T removeEntity)
        {
            var result = _context.Set<T>().Remove(removeEntity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
