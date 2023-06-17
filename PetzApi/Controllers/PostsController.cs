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
        public IActionResult Get()
        {
            return Ok(_postsRepository.GetAll());
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
        public IActionResult Get(int id)
        {
            var post = _postsRepository.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        // POST: api/Posts
        [HttpPost]
        public IActionResult Post(Posts post)
        {
            _postsRepository.Add(post);
            return Ok(post);
        }

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Posts post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            _postsRepository.Update(post);
            return Ok(post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _postsRepository.DeletePost(id);
            return NoContent();
        }

        // DELETE: api/Posts/5
        [HttpDelete("PostWithPets{id}")]
        public IActionResult DeletePostWithPets(int id)
        {
            _postsRepository.DeletePostWithPets(id);
            return NoContent();
        }

        // GET: api/PostsWithPets/postId
        [HttpGet("PostWithPets/{postId}")]
        public IActionResult GetPostWithPets(int postId)
        {
            return Ok(_postsRepository.GetPostWithPets(postId));
        }

        // GET: api/PostsWithPets/
        [HttpGet("PostsWithPets")]
        public IActionResult GetAllPostsWithPets()
        {
            return Ok(_postsRepository.GetAllPostsWithPets());
        }


        // POST: api/Posts
        [HttpPost("pets")]
        public IActionResult PetPosts(PetPosts petPosts)
        {
            _postsRepository.AddPetPosts(petPosts);
            return Ok(petPosts);
        }
    }

   
}
