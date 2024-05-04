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
            const int initialLoadCount = 20; // Количество пользователей для первоначальной загрузки
            var users = _userDataService.GenerateUserData(initialLoadCount + (page - 1) * 10, errorRate, seed, region);
            ViewBag.NextPage = page + 1; // Устанавливаем номер следующей страницы
            return PartialView("_UserListPartial", users);
        }

        public IActionResult ExportToCsv(int count, double errorRate, int seed, string region, int page)
        {
            // Генерируем все данные на основе переданных параметров
            var users = _userDataService.GenerateUserData(20+((page-1)*10), errorRate, seed, region);

            // Формируем CSV из всех данных с помощью CsvHelper
            var csvContent = CsvFormatter.Format(users);

            // Конвертируем строку в байты UTF-8
            var csvBytes = Encoding.UTF8.GetBytes(csvContent);

            // Создаем временный файл и сохраняем в него данные
            var tempFilePath = Path.GetTempFileName();
            System.IO.File.WriteAllBytes(tempFilePath, csvBytes);

            // Отправляем файл CSV как содержимое ответа
            return File(System.IO.File.ReadAllBytes(tempFilePath), "text/csv", $"all_users.csv");
        }
    }
}