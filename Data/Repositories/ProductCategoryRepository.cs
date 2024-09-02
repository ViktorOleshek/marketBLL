using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProductCategoryRepository : AbstractRepository, IProductCategoryRepository
    {
        private readonly DbSet<ProductCategory> dbSet;

        public ProductCategoryRepository(TradeMarketDbContext context)
            : base(context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.dbSet = context.Set<ProductCategory>();
        }

        public async Task AddAsync(ProductCategory entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Delete(ProductCategory entity)
        {
            dbSet.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            if(id > 0)
            {
                var entity = await dbSet.FindAsync(id);
                if(entity != null)
                {
                    Delete(entity);
                }
            }
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<ProductCategory> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Update(ProductCategory entity)
        {
            dbSet.Update(entity);
        }
    }
}
