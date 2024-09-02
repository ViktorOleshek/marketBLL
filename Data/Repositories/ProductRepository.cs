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
    public class ProductRepository : AbstractRepository<Product
        >, IProductRepository
    {

        public ProductRepository(TradeMarketDbContext context)
            : base(context)
        {
            ArgumentNullException.ThrowIfNull(context);
        }

        public async Task<IEnumerable<Product>> GetAllWithDetailsAsync()
        {
            return await Context.Set<Product>()
                .Include(e => e.ReceiptDetails)
                .Include(e => e.Category)
                .ToListAsync();
        }

        public Task<Product> GetByIdWithDetailsAsync(int id)
        {
            return Context.Set<Product>()
                .Include(e => e.ReceiptDetails)
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
