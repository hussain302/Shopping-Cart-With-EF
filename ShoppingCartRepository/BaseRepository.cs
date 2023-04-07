using Microsoft.EntityFrameworkCore;
using ShoppingCartDataAccessLayer.ShoppingCartContext;
using ShoppingCartInterfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        protected DbSet<T> Entities;
        public BaseRepository(ApplicationDbContext context)
        {
            this.context = context;
            Entities = context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return Entities.AsEnumerable<T>();
        }
        public virtual async Task<IEnumerable<T>> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = Entities;
            if (filter != null)
            {
                query = await Task.Run (() => query.Where(filter));
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }
        public virtual List<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return query.ToList();
        }
        public async Task<T> Find(int id)
        {
            var find = await Entities.FindAsync(id);
            if (find == null) return null;
            else return find;
        }
        public async Task<int> Add(T entity)
        {
            if (entity == null)
            {
              
                throw new ArgumentNullException("entity");
            }

            await Entities.AddAsync(entity);
            int response = await context.SaveChangesAsync();
            return response;
        }
        public async Task<int> Update(T entity)
        {
            if (entity == null)
            {            
                throw new ArgumentNullException("entity");
            }
            context.Update(entity);
            int response = await context.SaveChangesAsync();
            return response;
        }
        public async Task<int> Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");                
            }

            Entities.Remove(entity);
            int response = await context.SaveChangesAsync();
            return response;
        }
        public async Task<int> Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Entities.Remove(entity);
            int response = await context.SaveChangesAsync();
            return response;
        }
        public IEnumerable<T> LoadReference(Expression<Func<T, string>> refs)
        {
            return Entities.Include(refs);
        }
    }
}