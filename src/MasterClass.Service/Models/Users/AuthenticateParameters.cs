using System;
using System.Collections.Generic;
using System.Text;

namespace MasterClass.Service.Models.Users
{
    public class AuthenticateParameters
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
