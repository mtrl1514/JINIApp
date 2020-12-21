using JINIApp.Server.Data;
using JINIApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace JINIApp.Server.Models
{
    public class ItemsRepository : SqlRepository<Item>, IItemsRepository
    {
        public ItemsRepository(JINIAppServerContext context)
            : base(context)
        {
        }

        public override IQueryable<Item> GetAll()
        {
            return EfDbSet;
        }

        public override async Task<Item> GetById(object id)
        {
            return await GetAll().SingleOrDefaultAsync(o => o.ID == (int)id);
        }

        public async Task Insert(Item item)
        {
            await EfDbSet.AddAsync(item);
        }

        public async Task Update(Item item)
        {
            var entry = Context.Entry(item);
            if (entry.State == EntityState.Detached)
            {
                var attachedItem = await GetById(item.ID);
                if (attachedItem != null)
                {
                    Context.Entry(attachedItem).CurrentValues.SetValues(item);
                }
                else
                {
                    entry.State = EntityState.Modified;
                }
            }
        }

        public void Delete(Item item)
        {
            EfDbSet.Remove(item);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }

    public interface IItemsRepository
    {
        Task Insert(Item item);
        Task Update(Item item);
        void Delete(Item item);
        void Save();
    }
}