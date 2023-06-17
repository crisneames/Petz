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
public class PetsController : ControllerBase
{
    private readonly IPetsRepository _petsRepository;

    public PetsController(IPetsRepository petsRepository)
    {
        _petsRepository = petsRepository;
    }
    // GET: pets
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_petsRepository.GetAllPets());
    }


    // GET: Pets/Details/5
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var pets = _petsRepository.GetById(id);
        if (pets == null)
        {
            return NotFound();
        }
        return Ok(pets);
    }

    // POST: api/Pets
    [HttpPost]
    public IActionResult Post(Pets pet)
    {
        _petsRepository.Add(pet);
        return CreatedAtAction("Get", new { id = pet.Id }, pet);
    }

    // PUT: api/Pets/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, Pets pet)
    {
        if (id != pet.Id)
        {
            return BadRequest();
        }

        _petsRepository.Update(pet);
        return Ok(pet);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _petsRepository.Delete(id);
        return NoContent();
    }

    // GET: pets
    [HttpGet("user/{UserId}")]
    public IActionResult GetPetsByUser(int UserId)
    {
        return Ok(_petsRepository.GetPetsByUser(UserId));
    }


}


