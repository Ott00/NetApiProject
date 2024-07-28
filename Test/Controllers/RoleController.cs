using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Database;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        public RoleController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var allRoles = _dbContext.Roles.ToList();
            return Ok(allRoles);
        }

    }
}
