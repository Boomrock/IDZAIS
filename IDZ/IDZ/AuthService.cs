using System.Net.Http.Json;
using System.Text;

namespace IDZ
{
    public class AuthService
    {
        private readonly HttpClient _client;
        private readonly string _uriTokenService;
        private readonly StringBuilder _uriResponse;

        public AuthService()
        {
            _client = new HttpClient();
            _uriTokenService = Environment.GetEnvironmentVariable("AuthTokenService");
            _uriResponse = new StringBuilder();
        }

        public bool VerificationToken(string token)
        {
            var task = VerificationTokenAsync(token);
            task.Wait();
            return task.Result;
        }

        public async Task<bool> VerificationTokenAsync(string token)
        {
            _uriResponse.Clear();
            _uriResponse.Append(_uriTokenService);
            _uriResponse.Append(token);
            var uri = _uriResponse.ToString();

            var result = await _client.GetFromJsonAsync<bool>(uri);

            return result;
        }
    }
}
