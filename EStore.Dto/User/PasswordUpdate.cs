using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Dto.User
{
    public class PasswordUpdate
    {
        public string Authorized { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfrim { get; set; }
    }
}
