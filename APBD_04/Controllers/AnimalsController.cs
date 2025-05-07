using Microsoft.AspNetCore.Mvc;
using APBD_04.Data;
using System.Collections.Generic;
using System;
using System.Linq;
using APBD_04.Models;

namespace APBD_04.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IClinicRepository _repository;

    public AnimalsController(IClinicRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Animal>> GetAnimals()
    {
        return Ok(_repository.GetAllAnimals());
    }

    [HttpGet("{id}")]
    public ActionResult<Animal> GetAnimal(int id)
    {
        var animal = _repository.GetAnimalById(id);
        if (animal == null)
            return NotFound();

        return Ok(animal);
    }

    [HttpGet("search")]
    public ActionResult<IEnumerable<Animal>> SearchByName([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Należy podać nazwę do wyszukiwania");

        return Ok(_repository.FindAnimalsByName(name));
    }

    [HttpPost]
    public ActionResult<int> CreateAnimal(AnimalDTO animalDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = _repository.AddAnimal(animalDto);
        return CreatedAtAction(nameof(GetAnimal), new { id }, id);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAnimal(int id, AnimalDTO animalDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_repository.UpdateAnimal(id, animalDto))
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAnimal(int id)
    {
        if (!_repository.DeleteAnimal(id))
            return NotFound();

        return NoContent();
    }

    [HttpGet("{animalId}/visits")]
    public ActionResult<IEnumerable<Visit>> GetAnimalVisits(int animalId)
    {
        if (_repository.GetAnimalById(animalId) == null)
            return NotFound($"Zwierzę o ID {animalId} nie zostało znalezione");

        return Ok(_repository.GetAnimalVisits(animalId));
    }

    [HttpPost("{animalId}/visits")]
    public ActionResult<int> AddVisit(int animalId, VisitDTO visitDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var visitId = _repository.AddVisit(animalId, visitDto);
        if (visitId == -1)
            return NotFound($"Zwierzę o ID {animalId} nie zostało znalezione");

        return CreatedAtAction(nameof(GetAnimalVisits), new { animalId }, visitId);
    }
}
