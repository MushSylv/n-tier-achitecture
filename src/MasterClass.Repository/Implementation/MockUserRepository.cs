using MasterClass.Repository.Interface;
using MasterClass.Repository.Models.Users;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterClass.Repository
{
    public class MockUserRepository : IUserRepository
    {
        private readonly MockUsers _mock;

        public MockUserRepository(IOptions<MockUsers> mock) => _mock = mock.Value;
        public User GetUser(string login)
        {
            return _mock.Users.SingleOrDefault(user => user.Login == login);
        }
    }
}
