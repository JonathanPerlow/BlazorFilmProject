namespace NFLXV.Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly IDbService _db;

        public FilmsController(IDbService db) => _db = db;

        // GET: api/<FilmsController>
        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                _db.Include<Film>();
                _db.IncludeRef<FilmGenres>();
                _db.IncludeRef<SimilarFilm>();
                var films = await _db.GetAsync<Film, FilmInfoDTO>();
                return Results.Ok(films);
            }
            catch 
            {
                return Results.NotFound("Could not find any Films");
            }
        }

        // GET api/<FilmsController>/5
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            try
            {
                _db.Include<Film>();
                _db.IncludeRef<FilmGenres>();
                _db.IncludeRef<SimilarFilm>();
                var films = await _db.SingleAsync<Film, FilmInfoDTO>(f => f.Id == id);
                return Results.Ok(films);
            }
            catch
            {
                return Results.NotFound("Could not find that Film");
            }
        }

        // POST api/<FilmsController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] FilmCreateDTO dto)
        {
            try
            {
                var film = await _db.AddAsync<Film, FilmCreateDTO>(dto);
                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest();

                return Results.Created(_db.GetURL(film), film);
            }
            catch
            {
                return Results.BadRequest("Could not add the film");
            }
        }

        // PUT api/<FilmsController>/5
        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] FilmEditDTO dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return Results.BadRequest($"Id mismatch. URI ID: {id} DTO Id: {dto.Id}");
                }
                var exists = await _db.AnyAsync<Director>(d => d.Id == dto.DirectorId);
                if (!exists) return Results.NotFound("Director not found");

                exists = await _db.AnyAsync<Film>(d => d.Id == id);
                if (!exists) return Results.NotFound("Film not found");

                _db.Update<Film, FilmEditDTO>(id, dto);
                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest();

                return Results.NoContent();
            }
            catch
            {
                return Results.BadRequest("Could not update the Film");
            }
        }

        // DELETE api/<FilmsController>/5
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                var exists = await _db.AnyAsync<Film>(d => d.Id == id);
                if (!exists) return Results.NotFound("Film not found");

                var success = await _db.DeleteAsync<Film>(id);
                if (!success) return Results.NotFound("Film could not be deleted");

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
