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
    public class ProductRepository : AbstractRepository, IProductRepository
    {
        private readonly DbSet<Product> dbSet;

        public ProductRepository(TradeMarketDbContext context)
            : base(context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.dbSet = context.Set<Product>();
        }

        public async Task AddAsync(Product entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Delete(Product entity)
        {
            dbSet.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (id > 0)
            {
                var entity = await dbSet.FindAsync(id);
                if (entity != null)
                {
                    Delete(entity);
                }
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllWithDetailsAsync()
        {
            return await dbSet
                .Include(e => e.ReceiptDetails)
                .Include(e => e.Category)
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public Task<Product> GetByIdWithDetailsAsync(int id)
        {
            return dbSet
                .Include(e => e.ReceiptDetails)
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Update(Product entity)
        {
            dbSet.Update(entity);
        }
    }
}
