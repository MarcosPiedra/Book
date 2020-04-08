using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Books.Domain
{
    public class UserService : IUserService
    {
        private List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "UserNameX", Password = "P@assw0rd" }
        };

        public IEnumerable<User> GetUsers() => _users.Select(u => { u.Password = null; return u; });
    }
}
