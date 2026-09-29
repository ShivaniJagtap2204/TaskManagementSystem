using DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetTasks()
        {
            var tasks = _context.Tasks.ToList();

            return Ok(tasks);
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        public IActionResult CreateTask(TaskItem task)
        {
            task.CreatedDate = DateTime.Now;

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return Ok(task);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public IActionResult UpdateTask(int id, TaskItem task)
        {
            var existingTask = _context.Tasks.Find(id);

            if (existingTask == null)
                return NotFound();

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.AssignedTo = task.AssignedTo;
            existingTask.AssignedBy = task.AssignedBy;
            existingTask.TeamId = task.TeamId;
            existingTask.Priority = task.Priority;
            existingTask.Status = task.Status;
            existingTask.Deadline = task.Deadline;

            _context.SaveChanges();

            return Ok(existingTask);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
                return NotFound();

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return Ok("Task deleted successfully");
        }
    }
}
