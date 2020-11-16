﻿using MasterClass.Repository.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterClass.Service.Models.Users
{
    public class AuthenticatedUser
    {
        public int Id { get; }
        public string Token { get; }

        private AuthenticatedUser(int id, string token)
        {
            Id = id;
            Token = token;
        }

        internal static AuthenticatedUser Create(User user, string token)
            => user == null ? null : new AuthenticatedUser(user.Id, token);
    }
}
