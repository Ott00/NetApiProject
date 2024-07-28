using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Database;
using Test.Models;
using Test.Entities;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        public UserController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var allUsers = _dbContext.Users.ToList();
            return Ok(allUsers);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetUserById(Guid id)
        {
            var user = _dbContext.Users.Find(id);
            if (user is null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser(AddUserDto addUserDto)
        {
            var userEntity = new User()
            {
                Name = addUserDto.Name,
                Email = addUserDto.Email,
                Phone = addUserDto.Phone,
                Salary = addUserDto.Salary
            };

            _dbContext.Users.Add(userEntity);
            _dbContext.SaveChanges();

            return Ok(userEntity);
        }

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateUser(Guid id,[FromBody] UpdateUserDto updateUserDto)
        {
            var user = _dbContext.Users.Find(id);
            if (user is null) return NotFound();

            user.Name = updateUserDto.Name;
            user.Email = updateUserDto.Email;
            user.Phone = updateUserDto.Phone;
            user.Salary = updateUserDto.Salary;

            _dbContext.SaveChanges();

            return Ok(user);
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult DeleteUser(Guid id)
        {
            var user = _dbContext.Users.Find(id);
            if (user is null) return NotFound();

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges(); //Salvare sempre le modifiche altrimenti nel DB non persiste

            return Ok();
        }
    }
}
