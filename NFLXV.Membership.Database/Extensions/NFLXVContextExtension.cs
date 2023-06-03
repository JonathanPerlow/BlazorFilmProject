using static System.Net.WebRequestMethods;

namespace NFLXV.Membership.Database.Extensions;

public static class NFLXVContextExtension
{
    public static async Task SeedMembershipData(this IDbService service)
    {
		string description = "When industrialist and billionaire Tony Stark is kidnapped and forced to build weapons for the enemy.";

		try
		{
			await service.AddAsync<Director, DirectorCreateDTO>(new DirectorCreateDTO
			{
				Name = "Kenneth Branagh"
			}
			);

			await service.AddAsync<Director, DirectorCreateDTO>(new DirectorCreateDTO
			{
				Name = "Peyton Reed"
			}
			);
			await service.SaveChangesAsync();

			var director1 = await service.SingleAsync<Director, DirectorGetDTO>(d => d.Name == "Kenneth Branagh");

			var released1 = "2008-06-01"; //new DateTime(2008, 06, 01).ToString();

			await service.AddAsync<Film, FilmCreateDTO>(new FilmCreateDTO
			{
				Title = "Iron Man",
				Released = released1,
				ImageUrl = "/images/Iron Man.jpg",
				MarqueeUrl = "/images/Marvel Background.jpg",
				Free = true,
				Description = description,
				FilmUrl = "https://youtu.be/KAE5ymVLmZg",
				DirectorId = director1.Id,
			}
			);

			var director2 = await service.SingleAsync<Director, DirectorGetDTO>(d => d.Name == "Kenneth Branagh");

			var released2 = "2015-07-29";// new DateTime(2015, 07, 29).ToString();

			await service.AddAsync<Film, FilmCreateDTO>(new FilmCreateDTO
			{
				Title = "Ant Man",
				Released = released2,
				Free = true,
				ImageUrl = "/images/Ant Man.jpg",
				MarqueeUrl = "/images/Marvel Background.jpg",
                Description = description,
				FilmUrl = "https://youtu.be/pWdKf3MneyI",
				DirectorId = director2.Id,

			}
			);
			await service.SaveChangesAsync();

			await service.AddAsync<Genre, GenreCreateDTO>(new GenreCreateDTO
			{
				Name = "Action"
			}
			);

			await service.AddAsync<Genre, GenreCreateDTO>(new GenreCreateDTO
			{
				Name = "Drama"
			}
   );

			await service.AddAsync<Genre, GenreCreateDTO>(new GenreCreateDTO
			{
				Name = "Humour"
			}
   );
			await service.SaveChangesAsync();

			var film1 = await service.SingleAsync<Film, FilmGetDTO>(f => f.Title == "Iron Man");
			var genre1 = await service.SingleAsync<Genre, GenreGetDTO>(g => g.Name == "Action");
			await service.AddAsyncRefEntity<FilmGenres, FilmGenreDTO>(new FilmGenreDTO
			(
				film1.Id,
				genre1.Id
			)
			);

			await service.SaveChangesAsync();

			var parentfilm1 = await service.SingleAsync<Film, FilmGetDTO>(d => d.Title == "Ant Man");

			var similarfilm1 = await service.SingleAsync<Film, FilmGetDTO>(d => d.Title == "Iron Man");

			await service.AddAsyncRefEntity<SimilarFilm, SimilarFilmsDTO>(new SimilarFilmsDTO
			(
				parentfilm1.Id,
				similarfilm1.Id
			)
			);


			await service.AddAsyncRefEntity<SimilarFilm, SimilarFilmsDTO>(new SimilarFilmsDTO
			(
				similarfilm1.Id,
				parentfilm1.Id
			)
			);

			await service.SaveChangesAsync();


        }
        catch //(Exception ex)
		{

			//throw;
		}
    }
}
