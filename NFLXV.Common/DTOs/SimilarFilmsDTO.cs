namespace NFLXV.Common.DTOs;

public class SimilarFilmsDTO
{
    public int ParentFilmId { get; set; }
    public int SimilarFilmId { get; set; }

    public SimilarFilmsDTO(int parentFilmId, int similarFilmId) => (ParentFilmId, SimilarFilmId) = (parentFilmId, similarFilmId);

}


