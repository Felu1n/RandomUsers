using System.Text;
using Microsoft.AspNetCore.Mvc;
using RandomUsers.Utility;
using RandomUsers.Models;


namespace RandomUsers.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserDataService _userDataService;

        public UserController(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public IActionResult GenerateUsers()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerateUsers(int count, double errorRate, int seed, string region, int page = 1)
        {
            const int initialLoadCount = 20; 
            var users = _userDataService.GenerateUserData(initialLoadCount + (page - 1) * 10, errorRate, seed, region);
            ViewBag.NextPage = page + 1; 
            return PartialView("_UserListPartial", users);
        }

        public IActionResult ExportToCsv(int count, double errorRate, int seed, string region, int page)
        {
            
            var users = _userDataService.GenerateUserData(20+((page-1)*10), errorRate, seed, region);
            var csvContent = CsvFormatter.Format(users);
            var csvBytes = Encoding.UTF8.GetBytes(csvContent);
            var tempFilePath = Path.GetTempFileName();
            System.IO.File.WriteAllBytes(tempFilePath, csvBytes);
            return File(System.IO.File.ReadAllBytes(tempFilePath), "text/csv", $"all_users.csv");
        }
    }
}