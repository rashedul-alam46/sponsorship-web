using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Sponsorship.Models;

namespace Sponsorship.Services;

public class AccountAuthService
{
    private readonly HttpClient _http;
    private readonly string _baseUrl;

    public AccountAuthService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _baseUrl = config["ApiSettings:BaseUrl"] + "/accountauths";
    }

    public async Task<ApiAuthResponse<SignInResponseDto>> SignInAsync(SignInDto signInRequest)
    {
        try
        {
            var response = await _http.PostAsJsonAsync($"{_baseUrl}/sign-in", signInRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiAuthResponse<SignInResponseDto>>();
                return result ?? new ApiAuthResponse<SignInResponseDto>
                {
                    Success = false,
                    Message = "Failed to parse response"
                };
            }
            else
            {
                return new ApiAuthResponse<SignInResponseDto>
                {
                    Success = false,
                    Message = "Sign-in failed. Please check your credentials."
                };
            }
        }
        catch (Exception ex)
        {
            return new ApiAuthResponse<SignInResponseDto>
            {
                Success = false,
                Message = $"An error occurred: {ex.Message}"
            };
        }
    }

    public void StoreTokens(SignInResponseDto response)
    {
        if (response?.AccessToken != null)
        {
            // Store tokens in localStorage (would need to use JS interop in a real app)
            // For now, storing in browser storage via session
        }
    }
}

public class ApiAuthResponse<T>
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("data")]
    public T? Data { get; set; }
}
