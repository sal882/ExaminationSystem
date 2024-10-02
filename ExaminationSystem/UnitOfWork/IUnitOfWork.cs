using ExaminationSystem.Models;
using ExaminationSystem.Repositories;

using System.Linq.Expressions;
 
 

namespace ExaminationSystem.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : BaseEntity;
        Task<int> CompleteAsync();

    }
}
