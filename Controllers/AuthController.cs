using System.Threading.Tasks;
using Auth.API.Data;
using Auth.API.Dtos;
using Auth.API.Helpers;
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
        private readonly IJwtHelper _jwtHelper;

        public AuthController(IAuth repo, IMapper mapper, IJwtHelper jwtHelper)
        {
            _repository = repo;
            _mapper = mapper;
            _jwtHelper = jwtHelper;
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
        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(UserLoginDto user)
        {
            var userFromRepo = await _repository.Login(user.Email, user.Password);

            if (userFromRepo == null)
            {
                return Unauthorized();
            }

            var token = _jwtHelper.GenerateToken(userFromRepo);

            return Ok(new { AuthToken = token });
        }
    }
}