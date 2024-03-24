using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Services.Interfaces
{
    public interface IWriteService<TCreateEntity, TUpdateEntity>
    {
        public Task<TCreateEntity> AddAsync(TCreateEntity entity, string endpoint);
        public Task DeleteAsync(string id, string endpoint);
        public Task<TUpdateEntity> UpdateAsync(TUpdateEntity entity, string endpoint);
       
    }
}
