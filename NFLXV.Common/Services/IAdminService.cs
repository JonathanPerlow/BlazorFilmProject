namespace NFLXV.Common.Services
{
    public interface IAdminService
    {
        MembershipHttpClient _http { get;}

        Task CreateAsync<TDto>(string uri, TDto dto);
        Task DeleteAsync<TDto>(string uri);
        Task DeleteAsyncRef<TDto>(string uri, TDto dto);
        Task<List<TDto>> GetAsync<TDto>(string uri);
        Task<TDto> SingleAsync<TDto>(string uri);
        Task UpdateAsync<TDto>(string uri, TDto dto);
    }
}