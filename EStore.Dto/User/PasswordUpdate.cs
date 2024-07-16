using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Dto.User
{
    public class PasswordUpdate
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordConfrim { get; set; }
    }
}
