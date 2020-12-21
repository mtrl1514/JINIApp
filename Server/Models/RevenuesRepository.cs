using JINIApp.Server.Data;
using JINIApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JINIApp.Server.Models
{
    public class RevenuesRepository : SqlRepository<Revenue>, IRevenuesRepository
    {
        public RevenuesRepository(JINIAppServerContext context)
            : base(context)
        {
        }

        public override IQueryable<Revenue> GetAll()
        {
            return EfDbSet.Include(s => s.SalesOrder).Include(c => c.SalesOrder.Customer);
        }

        public override async Task<Revenue> GetById(object id)
        {
            return await GetAll().SingleOrDefaultAsync(o => o.ID == (int)id);
        }

        public IEnumerable<Revenue> GetForSalesOrder(int salesOrderId)
        {
            return GetAll().Where(o => o.SalesOrder.ID == salesOrderId);
        }

        public async Task Insert(Revenue revenue)
        {
            await EfDbSet.AddAsync(revenue);
        }

        public async Task Update(Revenue revenue)
        {
            var entry = Context.Entry(revenue);
            if (entry.State == EntityState.Detached)
            {
                var attachedRevenue = await GetById(revenue.ID);
                if (attachedRevenue != null)
                {
                    Context.Entry(attachedRevenue).CurrentValues.SetValues(revenue);
                }
                else
                {
                    entry.State = EntityState.Modified;
                }
            }
        }

        public void Delete(Revenue revenue)
        {
            EfDbSet.Remove(revenue);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }

    public interface IRevenuesRepository
    {
        Task Insert(Revenue revenue);
        Task Update(Revenue revenue);
        void Delete(Revenue revenue);
        void Save();
    }
}