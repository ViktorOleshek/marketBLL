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
    public class ReceiptDetailRepository : AbstractRepository, IReceiptDetailRepository
    {
        private readonly DbSet<ReceiptDetail> dbSet;

        public ReceiptDetailRepository(TradeMarketDbContext context)
            : base(context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.dbSet = context.Set<ReceiptDetail>();
        }

        public async Task AddAsync(ReceiptDetail entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Delete(ReceiptDetail entity)
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

        public async Task<IEnumerable<ReceiptDetail>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<IEnumerable<ReceiptDetail>> GetAllWithDetailsAsync()
        {
            return await dbSet
                .Include(e => e.Product)
                    .ThenInclude(e => e.Category)
                .Include(e => e.Receipt)
                .ToListAsync();
        }

        public async Task<ReceiptDetail> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Update(ReceiptDetail entity)
        {
            dbSet.Update(entity);
        }
    }
}
