using Books.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.Service
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
    }
}
