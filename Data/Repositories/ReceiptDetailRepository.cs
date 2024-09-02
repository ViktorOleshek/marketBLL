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
    public class ReceiptDetailRepository : AbstractRepository<ReceiptDetail>, IReceiptDetailRepository
    {

        public ReceiptDetailRepository(TradeMarketDbContext context)
            : base(context)
        {
            ArgumentNullException.ThrowIfNull(context);
        }

        public async Task<IEnumerable<ReceiptDetail>> GetAllWithDetailsAsync()
        {
            return await Context.Set<ReceiptDetail>()
                .Include(e => e.Product)
                    .ThenInclude(e => e.Category)
                .Include(e => e.Receipt)
                .ToListAsync();
        }
    }
}
