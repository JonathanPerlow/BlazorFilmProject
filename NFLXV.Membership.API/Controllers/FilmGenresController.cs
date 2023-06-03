// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


using AutoMapper;

namespace NFLXV.Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmGenresController : ControllerBase
    {
        private readonly IDbService _db;
        public FilmGenresController(IDbService db) => _db = db;

        // POST api/<FilmGenresController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] FilmGenreDTO dto)
        {
            try
            {
                var filmgenre = await _db.AddAsyncRefEntity<FilmGenres, FilmGenreDTO>(dto);
                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest();

                var node = typeof(FilmGenres).Name.ToLower();                                                  

                return Results.Created($"/{node}s/{dto}", filmgenre);                                                                                                                                                                                           
            }                                
            catch                          
            {
                return Results.BadRequest("Could not add the filmgenre");
            }
        }



        // DELETE api/<FilmGenresController>/5
        [HttpDelete]
        public async Task<IResult> Delete([FromBody] FilmGenreDTO dto)
        {
            try
            {
                var filmgenre = _db.DeleteAsyncRefEntity<FilmGenres, FilmGenreDTO>(dto);
                if (!filmgenre) return Results.NotFound();

                var success = await _db.SaveChangesAsync();
                if (success) return Results.NoContent();

            }
            catch
            {

            }
            return Results.BadRequest("Could not delete the filmgenre");

        }

    }
}
