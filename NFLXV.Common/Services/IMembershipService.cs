namespace NFLXV.Common.Services
{
    public interface IMembershipService
    {
        Task<DirectorGetDTO> GetDirectorAsyncById(int id);
        Task<List<DirectorGetDTO>> GetDirectorsAsync();
        Task<FilmInfoDTO> GetFilmAsyncById(int id);
        Task<List<FilmInfoDTO>> GetFilmsAsync();
    }
}