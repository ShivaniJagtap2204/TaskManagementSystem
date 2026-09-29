using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace TaskManagement.Web.Controllers
{
    public class TeamMembersController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TeamMembersController(IHttpClientFactory httpClientFactory)
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
                "https://localhost:7141/api/TeamMembers");

            if (!response.IsSuccessStatusCode)
                return Unauthorized();

            var members = await response.Content
                .ReadFromJsonAsync<List<TeamMemberViewModel>>();

            return View(members);
        }
    }

    public class TeamMemberViewModel
    {
        public int TeamMemberId { get; set; }
        public int TeamId { get; set; }
        public int UserId { get; set; }
    }
}
