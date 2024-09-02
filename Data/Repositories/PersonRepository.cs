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
    public class PersonRepository : AbstractRepository, IPersonRepository
    {
        private readonly DbSet<Person> dbSet;

        public PersonRepository(TradeMarketDbContext context)
            : base(context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.dbSet = context.Set<Person>();
        }

        public async Task AddAsync(Person entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Delete(Person entity)
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

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Update(Person entity)
        {
            dbSet.Update(entity);
        }
    }
}
