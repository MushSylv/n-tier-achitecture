using System;
using System.Collections.Generic;
using System.Text;

namespace MasterClass.Service.Interface.User
{
    public interface IAuthenticatedUser
    {
        int Id { get; }
        string Token { get; }
    }
}
