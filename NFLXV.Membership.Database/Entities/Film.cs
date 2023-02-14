namespace NFLXV.Membership.Database.Entities;

public class Film : IEntity
{
    public Film()
    {
        SimilarFilms = new List<SimilarFilms>();
        Genres = new List<Genre>();
    }

    public int Id { get; set; }

    [MaxLength(50), Required]
    public string Title { get; set; } = null!;

    public DateTime Released { get; set; } //default datetime(7) 
  
    public bool Free { get; set; } // By default, a Boolean property is displayed via a CheckEdit control.

    [MaxLength(200), Required]
    public string Description { get; set; } = null!;

    [MaxLength(1024), Required]
    public string FilmUrl { get; set; } = null!;

    public int DirectorId { get; set; }

    public virtual Director Director { get; set; } = null!;



    public virtual ICollection<SimilarFilms> SimilarFilms { get; set; } 
    
    public virtual ICollection<Genre> Genres { get; set; } 

}

