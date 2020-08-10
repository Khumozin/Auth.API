using System.Threading.Tasks;
using Auth.API.Data;
using Auth.API.Dtos;
using Auth.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _repository;
        private readonly IMapper _mapper;

        public AuthController(IAuth repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        // POST api/auth/register/
        [HttpPost("register")]
        public async Task<ActionResult<UserReadDto>> RegisterAsync(UserCreateDto userCreateDto)
        {
            if (await _repository.UserExists(userCreateDto.Email))
            {
                return BadRequest("Email already exists!");
            }

            var userFromRepo = await _repository.Register(userCreateDto, userCreateDto.Password);

            return Ok(_mapper.Map<UserReadDto>(userFromRepo));
        }

        // POST api/auth/login/
    }
}