using ExaminationSystem.Data;
using ExaminationSystem.Models;
using ExaminationSystem.Repositories;
using System.Collections;

namespace ExaminationSystem.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
<<<<<<< HEAD
        private readonly Dictionary<Type, object> _repository;
=======
        private Hashtable _repository; // (key) name or type of repo /(value) generic repo
>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8
        private readonly SystemContext _dbContext;

        public UnitOfWork(SystemContext dbContext)
        {
<<<<<<< HEAD
            _repository = new Dictionary<Type, object>();
=======

>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8
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
<<<<<<< HEAD

=======
>>>>>>> 4fa6b345b8ec4f703bc6668f3a752dfc833aefb8
    }
}
