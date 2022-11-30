using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;

namespace webNET_Hits_backend_aspnet_project_2.Services
{
    public class UserRepository<T> : IEfRepository<T> where T : BaseEntity
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
            var result = _context.Set<T>().FirstOrDefault(x => x.Id == id);

            if (result == null)
            {
                //todo: need to add logger
                return null;
            }

            return result;
        }

        public async Task<Guid> Add(T entity)
        {
            var result = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task<T> Edit(T newEntity)
        {
            var existingEntity = GetById(newEntity.Id);
            _context.Set<T>().Remove(existingEntity);
            await _context.Set<T>().AddAsync(newEntity);
            await _context.SaveChangesAsync();
            return newEntity;
        }
    }
}
