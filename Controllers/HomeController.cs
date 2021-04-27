using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CachingMVC.Models;
using CachingMVC.Services;

namespace CachingMVC.Controllers
{
    public class HomeController : Controller
    {
        UserService userService;
        public HomeController(UserService service)
        {
            userService = service;
            userService.Initialize();
        }
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public async Task<IActionResult> Index(int id)
        {
            User user = await userService.GetUser(id);
            if (user != null)
                return Content($"User: {user.Name}");
            return Content("User not found.");
        }
    }
}