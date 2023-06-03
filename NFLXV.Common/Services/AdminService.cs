using Newtonsoft.Json;
using System.Text;

namespace NFLXV.Common.Services
{
    public class AdminService : IAdminService
    {
        public  MembershipHttpClient _http { get; } 
        public AdminService(MembershipHttpClient httpClient) => _http = httpClient;

        public async Task<List<TDto>> GetAsync<TDto>(string uri)
        {
            try
            {
                using HttpResponseMessage response = await _http.Client.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                var result = System.Text.Json.JsonSerializer.Deserialize<List<TDto>>(await
                response.Content.ReadAsStreamAsync(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
                );

                return result ?? new List<TDto>(); // Return an empty List<TDto> if the result variable is null, otherwise return result.

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TDto> SingleAsync<TDto>(string uri)
        {
            try
            {
                using HttpResponseMessage response = await _http.Client.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                var result = System.Text.Json.JsonSerializer.Deserialize<TDto>(await
                response.Content.ReadAsStreamAsync(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
                );

                return result ?? default; // Return an empty List<TDto> if the result variable is null, otherwise return result.

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task CreateAsync<TDto>(string uri, TDto dto)
        {
            try
            {
                using StringContent jsonContent = new
                (
                    System.Text.Json.JsonSerializer.Serialize(dto),
                    Encoding.UTF8,
                    "application/json"
                );

                using HttpResponseMessage response = await _http.Client.PostAsync(uri, jsonContent);
                response.EnsureSuccessStatusCode();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateAsync<TDto>(string uri, TDto dto)
        {
            try
            {
                //var request = new HttpRequestMessage(HttpMethod.Put, uri);
                //request.Content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                //HttpResponseMessage response = _http.Client.Send(request);
                //response.EnsureSuccessStatusCode();
                using StringContent jsonContent = new
                (
                    JsonConvert.SerializeObject(dto),
                    Encoding.UTF8,
                    "application/json"
                );

                using HttpResponseMessage response = await _http.Client.PutAsync(uri, jsonContent);
                response.EnsureSuccessStatusCode();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteAsync<TDto>(string uri)
        {
            try
            {

                using HttpResponseMessage response = await _http.Client.DeleteAsync(uri);
                response.EnsureSuccessStatusCode();



                 // Return an empty List<TDto> if the result variable is null, otherwise return result.

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task DeleteAsyncRef<TDto>(string uri, TDto dto)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, uri);
                request.Content = new StringContent(JsonConvert.SerializeObject(dto),  Encoding.UTF8, "application/json");
                HttpResponseMessage response = _http.Client.Send(request);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {

                throw;
            }
        }




    }
}
