using JINIApp.Server.Data;
using JINIApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JINIApp.Server.Models
{
    public class SalesOrderItemsRepository : SqlRepository<SalesOrderItem>, ISalesOrderItemsRepository
    {
        public SalesOrderItemsRepository(JINIAppServerContext context)
            : base(context)
        {
        }

        public override IQueryable<SalesOrderItem> GetAll()
        {
            return EfDbSet.Include(i => i.Item).Include(s => s.SalesOrder).Include(c => c.SalesOrder.Customer);
        }

        public override async Task<SalesOrderItem> GetById(object id)
        {
            return await GetAll().SingleOrDefaultAsync(o => o.ID == (int)id);
        }

        public IEnumerable<SalesOrderItem> GetForSalesOrder(int salesOrderId)
        {
            return GetAll().Where(o => o.SalesOrder.ID == salesOrderId);
        }

        public async Task Insert(SalesOrderItem salesOrderItem)
        {
            await EfDbSet.AddAsync(salesOrderItem);
        }

        public async Task Update(SalesOrderItem salesOrderItem)
        {
            var entry = Context.Entry(salesOrderItem);
            if (entry.State == EntityState.Detached)
            {
                var attachedSalesOrderItem = await GetById(salesOrderItem.ID);
                if (attachedSalesOrderItem != null)
                {
                    Context.Entry(attachedSalesOrderItem).CurrentValues.SetValues(salesOrderItem);
                }
                else
                {
                    entry.State = EntityState.Modified;
                }
            }
        }

        public void Delete(SalesOrderItem salesOrderItem)
        {
            EfDbSet.Remove(salesOrderItem);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }

    public interface ISalesOrderItemsRepository
    {
        Task Insert(SalesOrderItem salesOrderItem);
        Task Update(SalesOrderItem salesOrderItem);
        void Delete(SalesOrderItem salesOrderItem);
        void Save();
    }
}