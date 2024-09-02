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
    public class CustomerRepository : AbstractRepository, ICustomerRepository
    {
        private readonly DbSet<Customer> dbSet;

        public CustomerRepository(TradeMarketDbContext context)
            : base(context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.dbSet = context.Set<Customer>();
        }

        public async Task AddAsync(Customer entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Delete(Customer entity)
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

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllWithDetailsAsync()
        {
            return await dbSet
                .Include(e => e.Person)
                .Include(e => e.Receipts)
                    .ThenInclude(r => r.ReceiptDetails)
                .ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public Task<Customer> GetByIdWithDetailsAsync(int id)
        {
            return dbSet
                .Include(e => e.Receipts)
                    .ThenInclude(r => r.ReceiptDetails)
                .Include(e => e.Person)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Update(Customer entity)
        {
            dbSet.Update(entity);
        }
    }
}
