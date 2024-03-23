using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Services.Interfaces
{
    public interface IWriteService<TEntity>
    {
        public Task<TEntity> AddAsync(TEntity entity, string endpoint);
        public Task DeleteAsync(string id, string endpoint);
        public Task<TEntity> UpdateAsync(string id, TEntity entity, string endpoint);
       
    }
}
