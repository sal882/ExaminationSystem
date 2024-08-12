using ExaminationSystem.Data;
using ExaminationSystem.Models;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
using System;
=======
>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8
using System.Linq.Expressions;

namespace ExaminationSystem.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly SystemContext _context;
<<<<<<< HEAD
        private readonly DbSet<T> _dbSet;
=======
>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8

        public GenericRepository(SystemContext Context)
        {
            _context = Context;
<<<<<<< HEAD
            _dbSet = Context.Set<T>();
=======
>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8
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
<<<<<<< HEAD

        public async Task<IEnumerable<T>> ListAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
=======
>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8
    }
}
