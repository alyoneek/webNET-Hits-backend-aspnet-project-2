using Microsoft.EntityFrameworkCore;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;

namespace webNET_Hits_backend_aspnet_project_2.Services
{
    public interface IEfRepository <T> where T : class //BaseEntity
    {
        List<T> GetAll();
        T GetById(Guid Id);
        Task<T> Add(T entity);
        Task EditRange(IEnumerable<T> newEntitiesRange);
        Task<T> Edit(T newEntity);
        Task<T> Delete(T removeEntity);
        Task DeleteRange(IEnumerable<T> removeEntitiesRange);
    }
}
