using DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetRoles()
        {
            var roles = _context.Roles.ToList();

            return Ok(roles);
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        public IActionResult CreateRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();

            return Ok(role);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public IActionResult UpdateRole(int id, Role role)
        {
            var existingRole = _context.Roles.Find(id);

            if (existingRole == null)
                return NotFound();

            existingRole.RoleName = role.RoleName;

            _context.SaveChanges();

            return Ok(existingRole);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public IActionResult DeleteRole(int id)
        {
            var role = _context.Roles.Find(id);

            if (role == null)
                return NotFound();

            _context.Roles.Remove(role);
            _context.SaveChanges();

            return Ok("Role deleted successfully");
        }
    }
}
