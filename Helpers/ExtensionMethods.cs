using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WebApi.Entities;

namespace WebApi.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static IEnumerable<Claim> GetRoles(this User user)
        {
            return user
                .Roles
                .Split(',')
                .Select(role => new Claim(ClaimTypes.Role, role));
        }

        public static User WithoutPassword(this User user)
        {
            user.Password = null;
            return user;
        }
    }
}