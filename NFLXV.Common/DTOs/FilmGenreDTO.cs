namespace NFLXV.Common.DTOs; 
public class FilmGenreDTO 
{
    public int FilmId { get; set; }
    public int GenreId { get; set; }

    public FilmGenreDTO(int filmId, int genreId) => (FilmId, GenreId) = (filmId, genreId);


}



