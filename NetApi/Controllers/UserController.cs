using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetApi.Database;
using NetApi.Models;
using NetApi.Entities;
using Microsoft.EntityFrameworkCore;
using NetApi.Repositories;
using AutoMapper;

namespace NetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserController(ApplicationDBContext dbContext, IUserRepository userRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var allUsers = await userRepository.GetAllAsync();
            return Ok(mapper.Map<List<UserDto>>(allUsers));
        }

        //Altro metodo per inserire una rotta direttamente col verbo REST [HttpGet("{id:Guid}")]
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user is null) return NotFound();
            return Ok(mapper.Map<UserDto>(user));
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserDto addUserDto)
        {
            var userDomain = mapper.Map<User>(addUserDto);
            userDomain = await userRepository.AddUserAsync(userDomain);
            var userReturn = mapper.Map<UserDto>(userDomain);
            return CreatedAtAction(nameof(GetUserById), new { id = userDomain.Id }, userReturn);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id,[FromBody] UpdateUserDto updateUserDto)
        {
            var userDomain = mapper.Map<User>(updateUserDto);
            userDomain = await userRepository.UpdateUserAsync(id, userDomain);
            if (userDomain is null) return NotFound();
            return Ok(mapper.Map<User>(userDomain));
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
