using Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public abstract class AbstractRepository
    {
        protected TradeMarketDbContext Context { get; }

        protected AbstractRepository(TradeMarketDbContext context)
        {
            this.Context = context;
        }
    }
}
