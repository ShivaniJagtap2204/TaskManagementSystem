using DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetTeams()
        {
            var teams = _context.Teams.ToList();

            return Ok(teams);
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        public IActionResult CreateTeam(Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();

            return Ok(team);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public IActionResult UpdateTeam(int id, Team team)
        {
            var existingTeam = _context.Teams.Find(id);

            if (existingTeam == null)
                return NotFound();

            existingTeam.TeamName = team.TeamName;
            existingTeam.ManagerId = team.ManagerId;

            _context.SaveChanges();

            return Ok(existingTeam);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public IActionResult DeleteTeam(int id)
        {
            var team = _context.Teams.Find(id);

            if (team == null)
                return NotFound();

            _context.Teams.Remove(team);
            _context.SaveChanges();

            return Ok("Team deleted successfully");
        }
    }
}
