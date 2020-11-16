using MasterClass.Service.Models.Users;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace MasterClass.Service.Interface.User
{
    public interface IUserService
    {
        AuthenticatedUser Authenticate(AuthenticateParameters authParams);
        ClaimsPrincipal SignIn(AuthenticateParameters authParams, string scheme);
    }
}
