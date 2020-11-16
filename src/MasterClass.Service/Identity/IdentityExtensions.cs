using MasterClass.Repository.Models.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace MasterClass.Service.Identity
{
    public static class IdentityExtensions
    {
        private const string JWTROLE_CLAIMNAME = "role";

        public static IEnumerable<Claim> GetJwtClaims(this User user, DateTimeOffset issuedAt)
        {
            foreach (var role in user.Roles)
            {
                yield return new Claim(JWTROLE_CLAIMNAME, role);
            }

            foreach (var rightClaim in user.GetMasterClassClaims())
            {
                yield return rightClaim;
            }

            yield return new Claim(JwtRegisteredClaimNames.Iat, issuedAt.ToUnixTimeSeconds().ToString());
            yield return new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString());
            yield return new Claim(JwtRegisteredClaimNames.Sub, user.Login);
            yield return new Claim(JwtRegisteredClaimNames.GivenName, user.Name);
            yield return new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
        }

        public static ClaimsPrincipal GetClaimsPrincipal(this User user, string scheme)
        {
            var identity = new GenericIdentity(user.Id.ToString(), scheme);
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Login));
            return new GenericPrincipal(identity, user.Roles);
        }

        private static IEnumerable<Claim> GetMasterClassClaims(this User user)
        {
            var rightClaims = user.Rights == null
                ? Enumerable.Empty<Claim>()
                : user.Rights.Select(right => new Claim(MasterClassClaims.RIGHTS_CLAIMNAME, right));

            return rightClaims
                .Concat(new[] { new Claim(ClaimTypes.DateOfBirth, user.BirthDate.ToString()) });
        }
    }
}
