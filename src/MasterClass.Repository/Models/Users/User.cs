using System;
using System.Collections.Generic;
using System.Text;

namespace MasterClass.Repository.Models.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string[] Roles { get; set; }
        public string[] Rights { get; set; }

    }
}
