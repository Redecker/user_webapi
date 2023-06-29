using backend_app.Services;
using backend_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace backend_app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IRandomUserService _userService; 
        private IMemoryCache _memoryCache;
        private const string cacheKey = "userList";

        public UserController(IRandomUserService randomUserServiceService, IMemoryCache memoryCache)
        {
            _userService = randomUserServiceService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            if(_userService == null)
            {
                return NotFound();
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            if (_memoryCache.TryGetValue(cacheKey, out List<User> users))
            {
                // Console.WriteLine("User found in cache");
            }
            else
            {
                users = _userService.GetAllUsers().ToList();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                        .SetPriority(CacheItemPriority.Normal)
                        .SetSize(1024);
                _memoryCache.Set(cacheKey, users, cacheEntryOptions);
            }
            stopwatch.Stop();
            Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
            return users;
        }

        [HttpGet]
        [Route("withoutcache")]
        public ActionResult<IEnumerable<User>> GetUsersWithoutCache()
        {
            if(_userService == null)
            {
                return NotFound();
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var users = _userService.GetAllUsers().ToList();
            stopwatch.Stop();
            Console.WriteLine("Elapsed Time Without Cache is {0} ms", stopwatch.ElapsedMilliseconds);
            return users;
        }

        [HttpGet]
        [Route("order")]
        public ActionResult<IEnumerable<User>> GetUsersOrder(int order)
        {
            if(_userService == null)
            {
                return NotFound();
            }

            var result = _userService.GetUsersByOrder((OrderEnum) order).ToList();
            return result;
        }

        [HttpGet]
        [Route("name")]
        public ActionResult<IEnumerable<User>> GetUsersName(string name)
        {
            if(_userService == null)
            {
                return NotFound();
            }

            var result = _userService.GetUsersByName(name).ToList();
            return result;
        }

    }
}