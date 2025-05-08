using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/animals")]
public class AnimalsController : ControllerBase
{
    [HttpGet("{id}")]
    public ActionResult<Animal> GetAnimalById(int id)
    {
        var animal = Data.Animals.FirstOrDefault(a => a.Id == id);
        return animal is null ? NotFound() : Ok(animal);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Animal>> GetAnimals([FromQuery] string? name)
    {
        var animals = Data.Animals.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(name))
            animals = animals.Where(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        return Ok(animals);
    }

    [HttpPost]
    public ActionResult AddAnimal(Animal animal)
    {
        animal.Id = Data.Animals.Count > 0 ? Data.Animals.Max(a => a.Id) + 1 : 1;
        Data.Animals.Add(animal);
        return CreatedAtAction(nameof(GetAnimalById), new { id = animal.Id }, animal);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateAnimal(int id, Animal updatedAnimal)
    {
        var animal = Data.Animals.FirstOrDefault(a => a.Id == id);
        if (animal is null) return NotFound();

        animal.Name = updatedAnimal.Name;
        animal.Category = updatedAnimal.Category;
        animal.Weight = updatedAnimal.Weight;
        animal.FurColor = updatedAnimal.FurColor;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteAnimal(int id)
    {
        var animal = Data.Animals.FirstOrDefault(a => a.Id == id);
        if (animal is null) return NotFound();

        Data.Animals.Remove(animal);
        return NoContent();
    }
}