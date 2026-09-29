using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace TaskManagement.Web.Controllers
{
    public class TasksController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TasksController(IHttpClientFactory httpClientFactory)
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
                "https://localhost:7141/api/Tasks");

            if (!response.IsSuccessStatusCode)
                return Unauthorized();

            var tasks = await response.Content
                .ReadFromJsonAsync<List<TaskViewModel>>();

            return View(tasks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TaskViewModel task)
        {
            var token = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Account");

            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer", token);

            var response = await client.PostAsJsonAsync(
                "https://localhost:7141/api/Tasks",
                task);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(task);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var token = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Account");

            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(
                $"https://localhost:7141/api/Tasks");

            if (!response.IsSuccessStatusCode)
                return Unauthorized();

            var tasks = await response.Content
                .ReadFromJsonAsync<List<TaskViewModel>>();

            var task = tasks?.FirstOrDefault(x => x.TaskId == id);

            if (task == null)
                return NotFound();

            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TaskViewModel task)
        {
            var token = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Account");

            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer", token);

            var response = await client.PutAsJsonAsync(
                $"https://localhost:7141/api/Tasks/{id}",
                task);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(task);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Account");

            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer", token);

            var response = await client.DeleteAsync(
                $"https://localhost:7141/api/Tasks/{id}");

            return RedirectToAction("Index");
        }


    }

    public class TaskViewModel
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AssignedTo { get; set; }
        public int AssignedBy { get; set; }
        public int? TeamId { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTime? Deadline { get; set; }
    }
}
