using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Auth.API.Data;
using Auth.API.Dtos;
using Auth.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser _repository;
        private readonly IMapper _mapper;

        public UsersController(IUser repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAllUsersAsync()
        {
            var usersFromRepo = await _repository.GetAllUsers();
            if (usersFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(usersFromRepo));
        }

        // GET api/users/{id}
        [HttpGet("{id}", Name = "GetUserByID")]
        public async Task<ActionResult<UserReadDto>> GetUserByIDAsync(Guid id)
        {
            var userFromRepo = await _repository.GetUserByID(id);
            if (userFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserReadDto>(userFromRepo));
        }

        // PUT api/users/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserAsync(Guid id, UserUpdateDto userUpdateDto)
        {
            var userFromRepo = await _repository.GetUserByID(id);
            if (userFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(userUpdateDto, userFromRepo);

            _repository.UpdateUser(userFromRepo);
            if (!await _repository.SaveChanges())
            {
                throw new Exception($"Updating user { id } failed on save!");
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUserAsync(Guid id)
        {
            var userFromRepo = await _repository.GetUserByID(id);
            if (userFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteUser(userFromRepo);

            if (!await _repository.SaveChanges())
            {
                throw new Exception($"Deleting user { id } failed on save!");
            }

            return NoContent();
        }
    }
}