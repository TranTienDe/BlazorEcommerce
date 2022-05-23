namespace BlazorEcommerce.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponse<string>> Login(UserLogin request)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", request);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<int>> Register(UserRegister request)
        {
            var response = await _http.PostAsJsonAsync("api/auth/register", request);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }
    }
}
