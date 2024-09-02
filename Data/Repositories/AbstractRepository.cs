using Data.Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public abstract class AbstractRepository<TEntity> 
        where TEntity : BaseEntity
    {
        protected TradeMarketDbContext Context { get; }

        protected AbstractRepository(TradeMarketDbContext context)
        {
            this.Context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public Task<TEntity> GetByIdAsync(int id) =>
            Context.Set<TEntity>().FindAsync(id).AsTask();

        public Task AddAsync(TEntity entity) =>
            Context.AddAsync(entity).AsTask();

        public void Delete(TEntity entity) =>
            Context.Remove(entity);

        public virtual async Task DeleteByIdAsync(int id)
        {
            if (id > 0)
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    Delete(entity);
                }
            }
        }

        public void Update(TEntity entity) =>
            Context.Update(entity);
    }
}
