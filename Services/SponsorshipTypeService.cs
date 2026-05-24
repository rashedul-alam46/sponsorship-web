using System.Net.Http.Json;
using Sponsorship.Models;

namespace Sponsorship.Services;

public class SponsorshipTypeService
{
    private readonly HttpClient _http;
    private readonly string _baseUrl;

    public SponsorshipTypeService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _baseUrl = config["ApiSettings:BaseUrl"] + "/sponsorshiptypes";
    }

    public async Task<List<SponsorshipTypeRead>> GetSponsorshipTypesAsync()
    {
        try
        {
            var url = _baseUrl;
            var httpResponse = await _http.GetAsync(url);

            if (!httpResponse.IsSuccessStatusCode)
            {
                // If API returns 4xx/5xx or invalid input, return empty list so UI shows no data available
                return new List<SponsorshipTypeRead>();
            }

            var apiResponse = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<List<SponsorshipTypeRead>>>();
            return apiResponse?.Data ?? new List<SponsorshipTypeRead>();
        }
        catch
        {
            // On exceptions (network, parse errors, etc.) return empty list
            return new List<SponsorshipTypeRead>();
        }
    }

    public async Task<SponsorshipTypeRead?> GetSponsorshipTypeAsync(Guid id)
    {
        var response = await _http.GetFromJsonAsync<ApiResponse<SponsorshipTypeRead>>($"{_baseUrl}/{id}");
        return response?.Data;
    }

    public async Task<bool> AddSponsorshipTypeAsync(SponsorshipTypeCreate sponsorshipType)
    {
        var response = await _http.PostAsJsonAsync(_baseUrl, sponsorshipType);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateSponsorshipTypeAsync(SponsorshipTypeUpdate sponsorshipType)
    {
        var response = await _http.PutAsJsonAsync($"{_baseUrl}/{sponsorshipType.TypeCode}", sponsorshipType);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateSponsorshipStatusAsync(SponsorshipRequestStatusUpdateDto statusUpdate)
    {
        var response = await _http.PutAsJsonAsync($"{_baseUrl}/{statusUpdate.SponsorshipId}/status", statusUpdate);
        return response.IsSuccessStatusCode;
    }

}

