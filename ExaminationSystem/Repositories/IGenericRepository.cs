using ExaminationSystem.Models;
using System.Linq.Expressions;

namespace ExaminationSystem.Repositories
{
    public interface IGenericRepository<T> where T :BaseEntity
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        T GetByID(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();
<<<<<<< HEAD
        Task<IEnumerable<T>> ListAllAsync();
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
=======
>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8
    }
}
