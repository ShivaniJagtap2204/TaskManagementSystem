using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using TaskManagement.Web.Models;

namespace TaskManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var usersResponse = await client.GetAsync(
                "https://localhost:7141/api/Users");

            var teamsResponse = await client.GetAsync(
                "https://localhost:7141/api/Teams");

            var tasksResponse = await client.GetAsync(
                "https://localhost:7141/api/Tasks");

            var notificationsResponse = await client.GetAsync(
                "https://localhost:7141/api/Notifications");

            var model = new DashboardViewModel();

            if (usersResponse.IsSuccessStatusCode)
            {
                var users = await usersResponse
                    .Content.ReadFromJsonAsync<List<object>>();

                model.UsersCount = users?.Count ?? 0;
            }

            if (teamsResponse.IsSuccessStatusCode)
            {
                var teams = await teamsResponse
                    .Content.ReadFromJsonAsync<List<object>>();

                model.TeamsCount = teams?.Count ?? 0;
            }

            if (tasksResponse.IsSuccessStatusCode)
            {
                var tasks = await tasksResponse
                    .Content.ReadFromJsonAsync<List<object>>();

                model.TasksCount = tasks?.Count ?? 0;
            }

            if (notificationsResponse.IsSuccessStatusCode)
            {
                var notifications = await notificationsResponse
                    .Content.ReadFromJsonAsync<List<object>>();

                model.NotificationsCount = notifications?.Count ?? 0;
            }

            return View(model);
        }
    }

    public class DashboardViewModel
    {
        public int UsersCount { get; set; }
        public int TeamsCount { get; set; }
        public int TasksCount { get; set; }
        public int NotificationsCount { get; set; }
    }
}
