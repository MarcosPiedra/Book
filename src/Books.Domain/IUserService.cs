using Books.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.Domain
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
    }
}
