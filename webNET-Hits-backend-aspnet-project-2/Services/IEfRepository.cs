﻿using webNET_Hits_backend_aspnet_project_2.Models.Entities;

namespace webNET_Hits_backend_aspnet_project_2.Services
{
    public interface IEfRepository <T> where T : BaseEntity
    {
        List<T> GetAll();
        T GetById(Guid Id);
        Task<Guid> Add(T entity);
    }
}
