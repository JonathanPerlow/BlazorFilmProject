using Microsoft.AspNetCore.Mvc;
using NFLXV.Membership.Database.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NFLXV.Membership.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SimilarFilmsController : ControllerBase
{
    private readonly IDbService _db;

    public SimilarFilmsController(IDbService db) => _db = db;

    // POST api/<SimilarFilmsController>
    [HttpPost]
    public async Task<IResult> Post([FromBody] SimilarFilmsDTO dto)
    {
        try
        {
            var similarfilm = await _db.AddAsyncRefEntity<SimilarFilm, SimilarFilmsDTO>(dto);
            var result = await _db.SaveChangesAsync();
            if (!result) return Results.BadRequest();
            var node = typeof(SimilarFilm).Name.ToLower();

            return Results.Created($"/{node}s/{dto}", similarfilm);

        }
        catch
        {
            return Results.BadRequest("Could not add the Similarfilm");
        }
    }



    // DELETE api/<SimilarFilmsController>/5
    [HttpDelete]
    public async Task<IResult> Delete([FromBody] SimilarFilmsDTO dto)
    {
        try
        {
            var similarfilm = _db.DeleteAsyncRefEntity<SimilarFilm, SimilarFilmsDTO>(dto);
            if (!similarfilm) return Results.NotFound();

            var success = await _db.SaveChangesAsync();
            if (success) return Results.NoContent();

        }
        catch
        {

        }
        return Results.BadRequest("Could not delete the similarfilm");
    }
}
