using System.Linq;
using System.Threading.Tasks;

namespace JINIApp.Server.Models
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        Task<T> GetById(object id);
    }
}