using System.ComponentModel.DataAnnotations.Schema;

namespace NFLXV.Membership.Database.Entities;
public class SimilarFilm : IReferenceEntity
{
    public int ParentFilmId { get; set; }
  
    public int SimilarFilmId { get; set; }


    public virtual Film Parent { get; set; } = null!;
    [ForeignKey("SimilarFilmId")]
    public virtual Film Similar { get; set; } = null!;



}
