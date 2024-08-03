using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetApi.Database;
using NetApi.Models;
using NetApi.Entities;
using Microsoft.EntityFrameworkCore;
using NetApi.Repositories;

namespace NetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IUserRepository userRepository;

        public UserController(ApplicationDBContext dbContext, IUserRepository userRepository)
        {
            _dbContext = dbContext;
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var allUsers = await userRepository.GetAllAsync();
            return Ok(allUsers);
        }

        //Altro metodo per inserire una rotta direttamente col verbo REST [HttpGet("{id:Guid}")]
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user is null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserDto addUserDto)
        {
            var userDomain = new User
            {
                Name = addUserDto.Name,
                Email = addUserDto.Email,
                Phone = addUserDto.Phone,
                Salary = addUserDto.Salary
            };

            await userRepository.AddUserAsync(userDomain);

            var userReturn = new User //Sarebbe da rimappare da domain a dto 
            {
                Id = userDomain.Id,
                Name = userDomain.Name,
                Email = userDomain.Email,
                Phone = userDomain.Phone,
                Salary = userDomain.Salary
            };

            return CreatedAtAction(nameof(GetUserById), new { id = userDomain.Id }, userReturn);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id,[FromBody] UpdateUserDto updateUserDto)
        {
            //Da dto a domain 
            var userDomain = new User
            {
                Name = updateUserDto.Name,
                Email = updateUserDto.Email,
                Phone = updateUserDto.Phone,
                Salary = updateUserDto.Salary
            };

            userDomain = await userRepository.UpdateUserAsync(id, userDomain);
            if (userDomain is null) return NotFound();

            //Sarebbe da rimappare da domain a dto 

            return Ok(userDomain);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await userRepository.DeleteUserAsync(id);
            if (user is null) return NotFound();
            return NoContent();
        }
    }
}
