using System.ComponentModel.DataAnnotations;

namespace NFLXV.Common.Models;

public class SimilarFilmsModel
{
    public int Id { get; set; }
    [MaxLength(50), Required]
    public string Title { get; set; } = null!;

}
