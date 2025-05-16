using System.Diagnostics;
using System.Linq.Expressions;
using ExamSystem.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace ExamSystem.DAL.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        public readonly MainContext _Context;
        protected readonly DbSet<T> _DbSet;

        public GenericRepository(MainContext context)
        {
            _Context = context;
            _DbSet = _Context.Set<T>();
        }

        public async Task<bool> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                await _Context.AddAsync(entity);
                return true;
            }
            catch (Exception E)
            {
                Debug.WriteLine(E.Message);
                return false;
            }
        }

        public bool Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                _Context.Remove(entity);
                return true;
            }
            catch (Exception E)
            {
                Debug.WriteLine(E.Message);
                return false;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _DbSet.ToListAsync();

        public async Task<IEnumerable<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _DbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            if (id < 0)
            {
                throw new InvalidOperationException($"This Id: {id} is invalid");
            }

            try
            {
                return await _DbSet.FindAsync(id);
            }
            catch (Exception E)
            {
                Debug.WriteLine(E.Message);
                throw;
            }
        }

        public bool Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                _Context.Update(entity);
                return true;
            }
            catch (Exception E)
            {
                Debug.WriteLine(E.Message);
                return false;
            }
        }
    }
}
