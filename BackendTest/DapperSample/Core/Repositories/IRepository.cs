using System.Collections.Generic;
using System.Threading.Tasks;

namespace STGenetics.Core.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
    }
}
