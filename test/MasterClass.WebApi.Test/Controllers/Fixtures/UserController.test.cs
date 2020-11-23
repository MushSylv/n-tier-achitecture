using MasterClass.Service.Interface.User;
using MasterClass.Service.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MasterClass.WebApi.Test.Controllers.Fixtures
{
    public class UserController : IClassFixture<UserControllerFixtureBuilder>
    {
        private readonly UserControllerFixtureBuilder _fixtureBuilder;

        public UserController(UserControllerFixtureBuilder fixtureBuilder)
        {
            _fixtureBuilder = fixtureBuilder;
        }

        #region Authenticate
        [Fact]
        public void Authenticate_Valid()
        {
            //Given
            var authParams = Mock.Of<AuthenticateParameters>();
            var authUser = Mock.Of<IAuthenticatedUser>();

            var userController = _fixtureBuilder
                .Initialize()
                .AddValidAuthentication(authParams, authUser)
                .Build()
                .GetService<UserController>();

            //When
            var actionResult = userController.Authenticate(authParams);

            //Then
            Assert.IsAssignableFrom<OkObjectResult>(actionResult);
            var model = (actionResult as OkObjectResult)?.Value;
            Assert.IsAssignableFrom<IAuthenticatedUser>(model);
            Assert.NotNull(model);
            Assert.Equal(authUser, model);
        }

        [Fact]
        public void Authenticate_Invalid()
        {
            //Given
            var authParams = Mock.Of<AuthenticateParameters>();

            var userController = _fixtureBuilder
                .Initialize()
                .AddInvalidAuthentication(authParams)
                .Build().GetService<UserController>();

            //When
            var actionResult = userController.Authenticate(authParams);

            //Then
            Assert.IsAssignableFrom<UnauthorizedResult>(actionResult);
            Assert.NotNull(actionResult);
        }

        #endregion
    }
}

