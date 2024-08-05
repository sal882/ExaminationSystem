using ExaminationSystem.Models;
using ExaminationSystem.Repositories;

namespace ExaminationSystem.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : BaseEntity;
        Task<int> CompleteAsync();
        
    }
}
