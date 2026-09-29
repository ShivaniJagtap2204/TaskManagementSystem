using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace TaskManagement.Web.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NotificationsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Account");

            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(
                "https://localhost:7141/api/Notifications");

            if (!response.IsSuccessStatusCode)
                return Unauthorized();

            var notifications = await response.Content
                .ReadFromJsonAsync<List<NotificationViewModel>>();

            return View(notifications);
        }
    }

    public class NotificationViewModel
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public int? TaskId { get; set; }
        public string NotificationType { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
    }
}
