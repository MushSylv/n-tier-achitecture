using MasterClass.Business.Interface;
using MasterClass.Repository.Interface;
using MasterClass.Repository.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterClass.Business.Implementation
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _userRepository;

        public UserBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User AuthenticateUser(string login, string password)
        {
            var user = _userRepository.GetUser(login);
            return user != null && user.Password == password ? user : null;
        }
    }
}
