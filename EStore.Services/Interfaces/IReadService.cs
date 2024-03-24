using System.Collections.Generic;
using System.Threading.Tasks;

namespace EStore.Services.Interfaces
{
    public interface IReadService<TEntity>
    {
        Task<List<TEntity>> GetAll(string apiUrl,string objectName);
        Task<TEntity> Get(string apiUrl,string id);
    }
}
