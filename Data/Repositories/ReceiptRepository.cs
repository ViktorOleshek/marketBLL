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
    public class ReceiptRepository : AbstractRepository, IReceiptRepository
    {
        private readonly DbSet<Receipt> dbSet;

        public ReceiptRepository(TradeMarketDbContext context)
            : base(context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.dbSet = context.Set<Receipt>();
        }

        public async Task AddAsync(Receipt entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Delete(Receipt entity)
        {
            dbSet.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (id > 0)
            {
                var entity = await dbSet.FindAsync(id);
                if(entity != null)
                {
                    Delete(entity);
                }
            }
        }

        public async Task<IEnumerable<Receipt>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<IEnumerable<Receipt>> GetAllWithDetailsAsync()
        {
            return await dbSet
                .Include(e => e.ReceiptDetails)
                    .ThenInclude(e => e.Product)
                        .ThenInclude(e => e.Category)
                .Include(e => e.Customer)
                .ToListAsync();
        }

        public async Task<Receipt> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public Task<Receipt> GetByIdWithDetailsAsync(int id)
        {
            return dbSet
                .Include(e => e.ReceiptDetails)
                    .ThenInclude(e => e.Product)
                        .ThenInclude(e => e.Category)
                .Include(e => e.Customer)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Update(Receipt entity)
        {
            dbSet.Update(entity);
        }
    }
}
