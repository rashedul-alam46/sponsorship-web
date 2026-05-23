using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Sponsorship.Models;

namespace Sponsorship.Services;

public class SponsorshipService
{
    private readonly HttpClient _http;
    private readonly string _baseUrl;

    public SponsorshipService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _baseUrl = config["ApiSettings:BaseUrl"] + "/sponsorshiprequests";
    }

    public async Task<List<SponsorshipReadDto>> GetSponsorshipRequestsAsync()
    {
        var response = await _http.GetFromJsonAsync<ApiResponse<List<SponsorshipReadDto>>>(_baseUrl);
        return response?.Data ?? new List<SponsorshipReadDto>();
    }

    public async Task<SponsorshipReadDto?> GetSponsorshipRequestAsync(Guid id)
    {
        var response = await _http.GetFromJsonAsync<ApiResponse<SponsorshipReadDto>>($"{_baseUrl}/{id}");
        return response?.Data;
    }

    public async Task<bool> AddSponsorshipRequestAsync(SponsorshipCreateDto sponsorshipRequest)
    {
        var response = await _http.PostAsJsonAsync(_baseUrl, sponsorshipRequest);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateSponsorshipRequestAsync(SponsorshipUpdateDto sponsorshipRequest)
    {
        var response = await _http.PutAsJsonAsync($"{_baseUrl}/{sponsorshipRequest.SponsorshipId}", sponsorshipRequest);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateSponsorshipStatusAsync(SponsorshipRequestStatusUpdateDto statusUpdate)
    {
        var response = await _http.PutAsJsonAsync($"{_baseUrl}/{statusUpdate.SponsorshipId}/status", statusUpdate);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteSponsorshipRequestAsync(Guid id)
    {
        var response = await _http.DeleteAsync($"{_baseUrl}/{id}");
        return response.IsSuccessStatusCode;
    }
}

internal class ApiResponse<T>
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("data")]
    public T? Data { get; set; }
}