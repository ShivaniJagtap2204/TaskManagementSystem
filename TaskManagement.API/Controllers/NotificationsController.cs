using DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetNotifications()
        {
            var notifications = _context.Notifications.ToList();

            return Ok(notifications);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            _context.SaveChanges();

            return Ok(notification);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateNotification(int id, Notification notification)
        {
            var existingNotification = _context.Notifications.Find(id);

            if (existingNotification == null)
                return NotFound();

            existingNotification.UserId = notification.UserId;
            existingNotification.TaskId = notification.TaskId;
            existingNotification.NotificationType = notification.NotificationType;
            existingNotification.Message = notification.Message;
            existingNotification.IsRead = notification.IsRead;

            _context.SaveChanges();

            return Ok(existingNotification);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteNotification(int id)
        {
            var notification = _context.Notifications.Find(id);

            if (notification == null)
                return NotFound();

            _context.Notifications.Remove(notification);
            _context.SaveChanges();

            return Ok("Notification deleted successfully");
        }
    }
}
