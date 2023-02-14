
using System.ComponentModel.DataAnnotations;

namespace NFLXV.Common.DTOs;

public class FilmGetDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;

    public DateOnly MyProperty { get; set; } //default datetime(7) 

    public virtual bool Free { get; set; } // By default, a Boolean property is displayed via a CheckEdit control.
    public string Description { get; set; } = null!;
    public string FilmUrl { get; set; } = null!;

    public int DirectorId { get; set; }

    public DirectorGetDTO Director { get; set; }

    public List<SimilarFilmsDTO> SimilarFilms { get; set; } = new();

    public List<GenreGetDTO> Genres { get; set; } = new();
}

public class FilmCreateDTO
{
    public string Title { get; set; } = null!;

    public DateOnly MyProperty { get; set; } //default datetime(7) 

    public virtual bool Free { get; set; } // By default, a Boolean property is displayed via a CheckEdit control.
    public string Description { get; set; } = null!;
    public string FilmUrl { get; set; } = null!;

    public int DirectorId { get; set; }
}

public class FilmEditDTO : FilmCreateDTO
{
    public int Id { get; set; }
}

public class FilmDeleteDTO 
{
    public int Id { get; set; }
}

