namespace NFLXV.Membership.Database.Entities;

public class Film : IEntity
{
    public Film()
    {
        SimilarFilms = new List<SimilarFilm>();
        Genres = new List<Genre>();
    }

    public int Id { get; set; }

    [MaxLength(50), Required]
    public string Title { get; set; } = null!;

    [MaxLength(50), Required]
    public string Released { get; set; } = null!; 

    [MaxLength(255)]
    public string ImageUrl { get; set; } = null!;

    [MaxLength(255)]
    public string MarqueeUrl { get; set; } = null!;
  
    public bool Free { get; set; } 

    [MaxLength(200), Required]
    public string Description { get; set; } = null!;

    [MaxLength(1024), Required]
    public string FilmUrl { get; set; } = null!;

    public int DirectorId { get; set; }

    public virtual Director Director { get; set; } = null!;



    public virtual ICollection<SimilarFilm> SimilarFilms { get; set; } 
    
    public virtual ICollection<Genre> Genres { get; set; } 

}

