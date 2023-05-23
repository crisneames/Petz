using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetzApi.Repositories;
using PetzApi.Models;


namespace PetzApi.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsRepository _postsRepository;

        public PostsController(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }


    
        // GET: api/Posts
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: Posts By User
        [HttpGet("PostsByUser/{id}")]
        public IActionResult GetPostsByUser(int id)
        {
            var posts = _postsRepository.GetPostsByUser(id);

            if (posts == null)
            {
                return NotFound();
            }
            return Ok(posts);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        //public IActionResult GetPostsByUser(int id)
        //{
        //    var posts = _postsRepository.GetPostsByUser(id);

        //    if (posts == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(posts);
       // }

        // POST: api/Posts
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
