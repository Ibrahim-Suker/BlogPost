using Infrasitructure.Data;
using Infrasitructure.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrasitructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet; // table name
        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public void Add(T entity, Action<string> LogAction)
        {
            LogAction?.Invoke($"{typeof(T).Name} Added successfully!");
            _dbSet.Add(entity);
            _context.SaveChanges();
        }
        public void Update(T entity, Action<string> LogAction)
        {
            LogAction?.Invoke($"{typeof(T).Name} Updated successfully!");
            _dbSet.Update(entity);
            _context.SaveChanges();
        }
        public void Delete(int id, Action<string> LogAction)
        {
            var item = GetById(id);
            if(item is not null)
            {
                LogAction?.Invoke($"{typeof(T).Name} Deleted successfully!");
                _dbSet.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                LogAction?.Invoke($"{typeof(T).Name} not found!");
                return;
            }

        }

        public Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>> criteria = null,
            Expression<Func<T, object>>[] includes = null)
        {
            IQueryable<T> query = _dbSet;
            if(criteria is not null)
            {
                query = query.Where(criteria);
            }
            if (includes is not null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query.ToListAsync();
        }
    }
}
