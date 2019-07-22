using Commons.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Domain
{
    public interface IMongoUserRepository<T>
    {
        void Add(T obj);
        T GetById(Int64 id);
        List<T> GetAll();
        void Update(T obj);
        void Remove(Int64 id);

        Task AddAsync(T obj);
        Task<T> GetByIdAsync(Int64 id);
        Task<List<T>> GetAllAsync();
        Task UpdateAsync(T obj);
        Task RemoveAsync(Int64 id);

    }
}
