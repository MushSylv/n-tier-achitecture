using MasterClass.Repository.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterClass.Business.Interface
{
    public interface IUserBusiness
    {
        User AuthenticateUser(string login, string password);
    }
}
