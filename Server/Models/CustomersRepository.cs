using JINIApp.Server.Data;
using JINIApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace JINIApp.Server.Models
{
    public class CustomersRepository : SqlRepository<Customer>, ICustomersRepository
    {
        public CustomersRepository(JINIAppServerContext context)
            : base(context)
        {
        }

        public override IQueryable<Customer> GetAll()
        {
            return EfDbSet;
        }

        public override async Task<Customer> GetById(object id)
        {
            return await GetAll().SingleOrDefaultAsync(o => o.ID == (int)id);
        }

        public async Task Insert(Customer customer)
        {
            await EfDbSet.AddAsync(customer);
        }

        public async Task Update(Customer customer)
        {
            var entry = Context.Entry(customer);
            if (entry.State == EntityState.Detached)
            {
                var attachedCustomer = await GetById(customer.ID);
                if (attachedCustomer != null)
                {
                    Context.Entry(attachedCustomer).CurrentValues.SetValues(customer);
                }
                else
                {
                    entry.State = EntityState.Modified;
                }
            }
        }

        public void Delete(Customer customer)
        {
            EfDbSet.Remove(customer);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }

    public interface ICustomersRepository
    {
        Task Insert(Customer customer);
        Task Update(Customer customer);
        void Delete(Customer customer);
        void Save();
    }
}