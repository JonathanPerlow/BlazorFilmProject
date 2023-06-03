using System.ComponentModel.DataAnnotations;

namespace NFLXV.Common.Models;

public class DirectorModel
{
    public int Id { get; set; }
    [MaxLength(80), Required]
    public string Name { get; set; } = null!;
}
