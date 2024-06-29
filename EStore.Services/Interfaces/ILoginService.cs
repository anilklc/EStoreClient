using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Services.Interfaces
{
    public interface ILoginService<TEntity>
    {
        Task<TEntity> Login(string apiUrl,object loginRequest);
    }
}
