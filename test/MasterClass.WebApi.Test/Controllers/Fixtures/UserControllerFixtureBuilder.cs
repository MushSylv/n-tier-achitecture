using MasterClass.Service.Interface.User;
using MasterClass.Service.Models.Users;
using MasterClass.WebApi.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterClass.WebApi.Test.Controllers.Fixtures
{
    public class UserControllerFixtureBuilder
    {
        private ServiceCollection _services;
        private readonly Mock<IUserService> _mockUserService;

        public UserControllerFixtureBuilder()
        {
            _services = new ServiceCollection();
            _mockUserService = new Mock<IUserService>();
        }

        public UserControllerFixtureBuilder Initialize()
        {
            _services.Clear();

            _services.AddSingleton<IUserService>(_mockUserService.Object);
            _services.AddSingleton<ISystemClock, SystemClock>();
            _services.AddTransient<IAuthenticatedUser, AuthenticatedUser>();
            _services.AddTransient<UserController>();

            return this;
        }

        public UserControllerFixtureBuilder AddValidAuthentication(AuthenticateParameters authParams, IAuthenticatedUser user)
        {
            _mockUserService.Setup(userService => userService.Authenticate(authParams))
                .Returns(user);

            return this;
        }

        public UserControllerFixtureBuilder AddInvalidAuthentication(AuthenticateParameters authParams)
        {
            _mockUserService.Setup(userService => userService.Authenticate(authParams))
                .Returns((IAuthenticatedUser)null);

            return this;
        }

        public IServiceProvider Build()
        {
            return _services.BuildServiceProvider();
        }
    }
}
