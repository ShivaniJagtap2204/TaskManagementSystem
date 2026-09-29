using DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetComments()
        {
            var comments = _context.Comments.ToList();

            return Ok(comments);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return Ok(comment);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateComment(int id, Comment comment)
        {
            var existingComment = _context.Comments.Find(id);

            if (existingComment == null)
                return NotFound();

            existingComment.TaskId = comment.TaskId;
            existingComment.UserId = comment.UserId;
            existingComment.CommentText = comment.CommentText;

            _context.SaveChanges();

            return Ok(existingComment);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteComment(int id)
        {
            var comment = _context.Comments.Find(id);

            if (comment == null)
                return NotFound();

            _context.Comments.Remove(comment);
            _context.SaveChanges();

            return Ok("Comment deleted successfully");
        }
    }
}
