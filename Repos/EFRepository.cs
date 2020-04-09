using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationApi.Repos
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _context;
        protected DbSet<T> _entities;

        public EFRepository(DbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Remove<T>(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entityToDelete = await _entities.FindAsync(id);
            _context.Remove<T>(entityToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetAsync(string id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<List<T>> GetAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> InsertAsync(T entity)
        {
            var newEntity = await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
            return newEntity.Entity;
        }

        public async Task UpdateAsync(T entity)
        {
            var entityToEdit = _entities.Find(entity.Id);

            if (entityToEdit == null)
                throw new ArgumentException($"Couldn't find matching {nameof(T)} with Id={entity.Id}");

            _context.Entry(entityToEdit).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
    }
}
