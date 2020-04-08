using Books.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.Service
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetUsers();
    }
}
