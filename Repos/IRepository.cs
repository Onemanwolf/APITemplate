using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationApi.Repos
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task DeleteAsync(T entity);
        Task DeleteAsync(string id);
        Task<T> GetAsync(string id);
        Task<List<T>> GetAsync();
        Task<T> InsertAsync(T entity);
        Task UpdateAsync(T entity);
    }
}