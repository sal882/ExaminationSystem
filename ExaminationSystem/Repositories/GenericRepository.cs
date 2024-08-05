using ExaminationSystem.Data;
using ExaminationSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace ExaminationSystem.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly SystemContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(SystemContext Context)
        {
            _context = Context;
            _dbSet = Context.Set<T>();
        }
        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().Where(T => !T.IsDeleted);
        }

        public T GetByID(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<IEnumerable<T>> ListAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
    }
}
