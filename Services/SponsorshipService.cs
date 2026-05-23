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

    public async Task<List<SponsorshipReadDto>> GetSponsorshipRequestsAsync(Guid userId, int roleId)
    {
        try
        {
            var url = $"{_baseUrl}?userId={userId}&roleId={roleId}";
            var httpResponse = await _http.GetAsync(url);

            if (!httpResponse.IsSuccessStatusCode)
            {
                // If API returns 4xx/5xx or invalid input, return empty list so UI shows no data available
                return new List<SponsorshipReadDto>();
            }

            var apiResponse = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<List<SponsorshipReadDto>>>();
            return apiResponse?.Data ?? new List<SponsorshipReadDto>();
        }
        catch
        {
            // On exceptions (network, parse errors, etc.) return empty list
            return new List<SponsorshipReadDto>();
        }
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

    public async Task<List<WorkflowHistoryReadDto>> GetSponsorshipHistoryAsync(Guid id)
    {
        var response = await _http.GetAsync($"{_baseUrl}/{id}/history");
        if (!response.IsSuccessStatusCode)
        {
            return new List<WorkflowHistoryReadDto>();
        }

        try
        {
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<WorkflowHistoryReadDto>>>();
            return apiResponse?.Data ?? new List<WorkflowHistoryReadDto>();
        }
        catch
        {
            return new List<WorkflowHistoryReadDto>();
        }
    }

    public async Task<bool> CancelSponsorshipRequestAsync(Guid id)
    {
        var response = await _http.PostAsync($"{_baseUrl}/{id}/cancel", null);
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