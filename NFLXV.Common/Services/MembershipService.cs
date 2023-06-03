namespace NFLXV.Common.Services;

public class MembershipService : IMembershipService
{
    private readonly MembershipHttpClient _http;

    public MembershipService(MembershipHttpClient http)
    {
        _http = http;
    }

    public async Task<List<FilmInfoDTO>> GetFilmsAsync()
    {
        try
        {
            bool freeOnly = false;
            HttpResponseMessage response = await _http.Client.GetAsync($"films?freeOnly={freeOnly}");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<List<FilmInfoDTO>>(await
            response.Content.ReadAsStreamAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
            );

            return result ?? new();
        }
        catch
        {

            return new();
        }
    }

    public async Task<FilmInfoDTO> GetFilmAsyncById(int id)
    {
        try
        {
            HttpResponseMessage response = await _http.Client.GetAsync($"films/{id}");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<FilmInfoDTO>(await
            response.Content.ReadAsStreamAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
            );

            return result ?? new();
        }
        catch
        {

            return  new();
        }
    }

    public async Task<DirectorGetDTO> GetDirectorAsyncById(int id)
    {
        try
        {
            HttpResponseMessage response = await _http.Client.GetAsync($"directors/{id}");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<DirectorGetDTO>(await
            response.Content.ReadAsStreamAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
            );

            return result ?? new();
        }
        catch
        {

            return new();
        }
    }

    public async Task<List<DirectorGetDTO>> GetDirectorsAsync()
    {
        try
        {
            bool freeOnly = false;
            HttpResponseMessage response = await _http.Client.GetAsync($"directors?");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<List<DirectorGetDTO>>(await
            response.Content.ReadAsStreamAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
            );

            return result ?? new();
        }
        catch
        {

            return new();
        }
    }
}
