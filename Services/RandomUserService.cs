using backend_app.Models;
using Newtonsoft.Json;

namespace backend_app.Services
{
    public class RandomUserService : IRandomUserService
    {
        private IList<User> _users;

        public RandomUserService()
        {
            GeneratesUserList();
        }

        private void GeneratesUserList()
        {
            _users = new List<User>();

            using var client = new HttpClient();
            var result = client.GetAsync("https://randomuser.me/api/?results=1000");
            
            Result result_class = JsonConvert.DeserializeObject<Result>(result.Result.Content.ReadAsStringAsync().Result);

            foreach(var user in result_class.results)
            {
               _users.Add(user); 
            }
        }

        public IEnumerable<User> GetAllUsers()
        {   
            Thread.Sleep(5000);
            return _users;
        }

        public IEnumerable<User> GetUsersByOrder(OrderEnum orderEnum)
        {
            if (orderEnum == OrderEnum.FirstName)
                return _users.OrderBy(o => o.name.first);
            if (orderEnum == OrderEnum.LastName)
                return _users.OrderBy(o => o.name.last);
            
            return _users;
        }
        public IEnumerable<User> GetUsersByName(string name)
        {
            return _users.Where(o => o.name.first.Contains(name));
        }
    }
}