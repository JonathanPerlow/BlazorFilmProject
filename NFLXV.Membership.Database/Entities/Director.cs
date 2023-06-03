namespace NFLXV.Membership.Database.Entities;

public class Director : IEntity
{
    public int Id{ get; set; }
    [MaxLength(80), Required]
    public string Name { get; set; } = null!;
    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}

