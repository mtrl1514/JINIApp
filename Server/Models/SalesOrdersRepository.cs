using JINIApp.Server.Data;
using JINIApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace JINIApp.Server.Models
{
    public class SalesOrdersRepository : SqlRepository<SalesOrder>, ISalesOrdersRepository
    {
        public SalesOrdersRepository(JINIAppServerContext context)
            : base(context)
        {
        }

        public override IQueryable<SalesOrder> GetAll()
        {
            return EfDbSet.Include("Customer");
        }

        public override async Task<SalesOrder> GetById(object id)
        {
            return await GetAll().SingleOrDefaultAsync(o => o.ID == (int)id);
        }

        public async Task Insert(SalesOrder salesOrder)
        {
            await EfDbSet.AddAsync(salesOrder);
        }

        public async Task Update(SalesOrder salesOrder)
        {
            var entry = Context.Entry(salesOrder);
            if (entry.State == EntityState.Detached)
            {
                var attachedSalesOrder = await GetById(salesOrder.ID);
                if (attachedSalesOrder != null)
                {
                    Context.Entry(attachedSalesOrder).CurrentValues.SetValues(salesOrder);
                }
                else
                {
                    entry.State = EntityState.Modified;
                }
            }
        }

        public void Delete(SalesOrder salesOrder)
        {
            EfDbSet.Remove(salesOrder);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }

    public interface ISalesOrdersRepository
    {
        Task Insert(SalesOrder salesOrder);
        Task Update(SalesOrder salesOrder);
        void Delete(SalesOrder salesOrder);
        void Save();
    }
}