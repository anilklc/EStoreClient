﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Dto.Worker
{
    public class UpdateUserPassword
    {
        public string Authorized { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfrim { get; set; }
    }
}
