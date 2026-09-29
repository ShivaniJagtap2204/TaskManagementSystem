using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace TaskManagement.Web.Controllers
{
    public class TeamsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TeamsController(IHttpClientFactory httpClientFactory)
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
                "https://localhost:7141/api/Teams");

            if (!response.IsSuccessStatusCode)
                return Unauthorized();

            var teams = await response.Content
                .ReadFromJsonAsync<List<TeamViewModel>>();

            return View(teams);
        }
    }

    public class TeamViewModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int ManagerId { get; set; }
    }
}
