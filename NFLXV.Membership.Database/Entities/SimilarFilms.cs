using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLXV.Membership.Database.Entities;
public class SimilarFilms : IReferenceEntity
{
    public int ParentFilmId { get; set; }

    [ForeignKey("SimilarFilmId")]
    public int SimilarFilmId { get; set; }


    public virtual Film ParentFilm { get; set; } = null!;

    public virtual Film SimilarFilm { get; set; } = null!;



}
