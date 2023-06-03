
using System.ComponentModel.DataAnnotations;

namespace NFLXV.Common.DTOs;

public class FilmGetDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;

    public string Released { get; set; } = null!; 
    public string ImageUrl { get; set; } = null!;

    public string MarqueeUrl { get; set; } = null!;
    public virtual bool Free { get; set; } 
    public string Description { get; set; } = null!;
    public string FilmUrl { get; set; } = null!;

    public int DirectorId { get; set; }

    public DirectorGetDTO Director { get; set; }


}

public class FilmCreateDTO
{
    public string Title { get; set; } = null!;

    public string Released { get; set; } = null!; 
    public string ImageUrl { get; set; } = null!;

    public string MarqueeUrl { get; set; } = null!;
    public virtual bool Free { get; set; } 
    public string Description { get; set; } = null!;
    public string FilmUrl { get; set; } = null!;

    public int DirectorId { get; set; }
}

public class FilmEditDTO : FilmCreateDTO
{
    public int Id { get; set; }
}

public class FilmInfoDTO : FilmGetDTO
{

    public List<SimilarFilmsDTO> SimilarFilms { get; set; } = new();

    public List<GenreGetDTO> Genres { get; set; } = new();

}

public class FilmDeleteDTO 
{
    public int Id { get; set; }
}

