using ExaminationSystem.Models;
using ExaminationSystem.Repositories;
<<<<<<< HEAD
using System.Linq.Expressions;
=======
>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8

namespace ExaminationSystem.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : BaseEntity;
        Task<int> CompleteAsync();
<<<<<<< HEAD

=======
        
>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8
    }
}
