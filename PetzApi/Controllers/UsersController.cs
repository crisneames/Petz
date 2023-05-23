using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetzApi.Repositories;
using PetzApi.Models;

namespace PetzApi.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        // GET: Users
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_usersRepository.GetAllUsers());
        }


        // GET: Users/Details/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var users = _usersRepository.GetById(id);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult Post(Users user)
        {
            _usersRepository.Add(user);
            return CreatedAtAction("Get", new { id = user.Id }, user);
        }

    // PUT: api/Users/5
    [HttpPut("{id}")]
        public IActionResult Put(int id, Users user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _usersRepository.Update(user);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _usersRepository.Delete(id);
            return NoContent();
        }
    }

