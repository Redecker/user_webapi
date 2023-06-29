using backend_app.Models;

namespace backend_app.Services
{
    public interface IRandomUserService
    {
        IEnumerable<User> GetAllUsers();

        IEnumerable<User> GetUsersByOrder(OrderEnum order);

        IEnumerable<User> GetUsersByName(string name);
    }
}