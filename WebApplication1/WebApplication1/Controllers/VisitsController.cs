using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/visits")]
public class VisitsController : ControllerBase
{
    [HttpGet("/api/animals/{animalId}/visits")]
    public ActionResult<IEnumerable<Visit>> GetVisits(int animalId)
    {
        var visits = Data.Visits.Where(v => v.AnimalId == animalId);
        return Ok(visits);
    }

    [HttpPost("/api/animals/{animalId}/visits")]
    public ActionResult AddVisit(int animalId, Visit visit)
    {
        var animal = Data.Animals.FirstOrDefault(a => a.Id == animalId);
        if (animal is null) return NotFound("Animal not found.");

        visit.Id = Data.Visits.Count > 0 ? Data.Visits.Max(v => v.Id) + 1 : 1;
        visit.AnimalId = animalId;
        Data.Visits.Add(visit);

        return CreatedAtAction(nameof(GetVisits), new { animalId = animalId }, visit);
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Visit>> GetAllVisits()
    {
        return Ok(Data.Visits);
    }
}