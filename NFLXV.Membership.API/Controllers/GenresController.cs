namespace NFLXV.Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IDbService _db;

        public GenresController(IDbService db) => _db = db;
        // GET: api/<GenresController>
        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                var films = await _db.GetAsync<Genre, GenreGetDTO>();
                return Results.Ok(films);
            }
            catch
            {
                return Results.NotFound("Could not find any Films");
            }
        }

        // GET api/<GenresController>/5
        [HttpGet("{id}")]
        public async Task<IResult>Get(int id)
        {
            try
            {
                var genre = await _db.SingleAsync<Genre, GenreGetDTO>(g => g.Id == id);
                return Results.Ok(genre);
            }
            catch (Exception)
            {

                return Results.BadRequest("Could not find that genre");
            }
        }

        // POST api/<GenresController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] GenreCreateDTO dto)
        {
            try
            {
                var film = await _db.AddAsync<Genre, GenreCreateDTO>(dto);
                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest();

                return Results.Created(_db.GetURL(film), film);
            }
            catch
            {
                return Results.BadRequest("Could not add the genre");
            }
        }

            // PUT api/<GenresController>/5
            [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] GenreEditDTO dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return Results.BadRequest($"Id mismatch. URI ID: {id} DTO Id: {dto.Id}");
                }
                var exists = await _db.AnyAsync<Genre>(d => d.Id == id);
                if (!exists) return Results.NotFound("Genre not found");

                _db.Update<Genre, GenreEditDTO>(id, dto);
                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest();

                return Results.NoContent();
            }
            catch
            {
                return Results.BadRequest("Could not update the genre");
            }
        }



        // DELETE api/<GenresController>/5
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                var exists = await _db.AnyAsync<Genre>(d => d.Id == id);
                if (!exists) return Results.NotFound("The genre could not be found");

                var success = await _db.DeleteAsync<Genre>(id);
                if (!success) return Results.NotFound("The genre could not be deleted");

                await _db.SaveChangesAsync();

                return Results.NoContent();
            }
            catch
            {
                return Results.BadRequest($"Failed to delete {id}");
            }
        }
    }
}
