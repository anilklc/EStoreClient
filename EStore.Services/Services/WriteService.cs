using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Services.Services
{
    public class WriteService<TEntity>
    {
        public Task<TEntity> AddAsync(TEntity item, string endpoint)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id, string endpoint)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync(string id, TEntity item, string endpoint)
        {
            throw new NotImplementedException();
        }
    }
}
