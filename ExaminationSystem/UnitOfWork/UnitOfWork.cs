using ExaminationSystem.Data;
using ExaminationSystem.Models;
using ExaminationSystem.Repositories;
using System.Collections;

namespace ExaminationSystem.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repository; // (key) name or type of repo /(value) generic repo
        private readonly SystemContext _dbContext;

        public UnitOfWork(SystemContext dbContext)
        {

            _dbContext = dbContext;
        }
        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {

            var key = typeof(T);                             //type of repository

            if (!_repository.ContainsKey(key))
            {
                var value = new GenericRepository<T>(_dbContext);   //repository

                _repository.Add(key, value);
            }

            return _repository[key] as IGenericRepository<T>;
        }
        public async Task<int> CompleteAsync()
        {
            //HandleSoftDelete();
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
            => _dbContext.Dispose();
    }
}
