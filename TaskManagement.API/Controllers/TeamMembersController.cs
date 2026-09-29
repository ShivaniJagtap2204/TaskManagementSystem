using DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMembersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeamMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetTeamMembers()
        {
            var teamMembers = _context.TeamMembers.ToList();

            return Ok(teamMembers);
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        public IActionResult CreateTeamMember(TeamMember teamMember)
        {
            _context.TeamMembers.Add(teamMember);
            _context.SaveChanges();

            return Ok(teamMember);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public IActionResult UpdateTeamMember(int id, TeamMember teamMember)
        {
            var existingMember = _context.TeamMembers.Find(id);

            if (existingMember == null)
                return NotFound();

            existingMember.TeamId = teamMember.TeamId;
            existingMember.UserId = teamMember.UserId;

            _context.SaveChanges();

            return Ok(existingMember);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public IActionResult DeleteTeamMember(int id)
        {
            var teamMember = _context.TeamMembers.Find(id);

            if (teamMember == null)
                return NotFound();

            _context.TeamMembers.Remove(teamMember);
            _context.SaveChanges();

            return Ok("Team member deleted successfully");
        }
    }
}
