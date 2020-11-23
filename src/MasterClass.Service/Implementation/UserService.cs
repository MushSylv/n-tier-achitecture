﻿using MasterClass.Business.Interface;
using MasterClass.Core.Class;
using MasterClass.Core.Tools;
using MasterClass.Repository.Models.Users;
using MasterClass.Service.Identity;
using MasterClass.Service.Interface.User;
using MasterClass.Service.Models.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MasterClass.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserBusiness _userBusiness;
        private readonly JwtOptions _jwtOptions;
        private readonly ISystemClock _clock;

        public UserService(IUserBusiness userBusiness, IOptions<JwtOptions> jwtOptions, ISystemClock clock)
        {
            _userBusiness = userBusiness;
            _jwtOptions = jwtOptions.Value;
            _clock = clock;
        }

        public IAuthenticatedUser Authenticate(AuthenticateParameters authParams)
        {
            var user = _userBusiness.AuthenticateUser(authParams.Login, authParams.Password);
            if (user != null)
            {
                var issuedAt = _clock.UtcNow;
                var jwtToken = _jwtOptions.Enabled
                    ? new JwtSecurityToken(
                        issuer: _jwtOptions.Issuer,
                        claims: user.GetJwtClaims(issuedAt),
                        expires: issuedAt.LocalDateTime.Add(_jwtOptions.Duration),
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_jwtOptions.Key.ToBytes()), SecurityAlgorithms.HmacSha256))
                    : null;

                return AuthenticatedUser.Create(user, jwtToken == null ? null : new JwtSecurityTokenHandler().WriteToken(jwtToken));
            }
            return null;
        }

        public ClaimsPrincipal SignIn(AuthenticateParameters authParams, string scheme)
        {
            var user = _userBusiness.AuthenticateUser(authParams.Login, authParams.Password);
            return user == null ? null : user.GetClaimsPrincipal(scheme);
        }
    }
}
