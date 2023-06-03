using System.ComponentModel.DataAnnotations;

namespace NFLXV.Common.Models;

public class GenreModel
{
    public int Id { get; set; }
    [MaxLength(80), Required]
    public string Name { get; set; } = null!;
}
