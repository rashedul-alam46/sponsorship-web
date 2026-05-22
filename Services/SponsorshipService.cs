using System.Net.Http.Json;
using Sponsorship.Models;

namespace Sponsorship.Services;

public class SponsorshipService
{
    private readonly HttpClient _http;
    private readonly string _baseUrl;

    public SponsorshipService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _baseUrl = config["ApiSettings:BaseUrl"] + "/sponsorship-requests";
    }

    public async Task<List<SponsorshipReadDto>> GetSponsorshipRequestsAsync()
        => await _http.GetFromJsonAsync<List<SponsorshipReadDto>>(_baseUrl) ?? new List<SponsorshipReadDto>();

    public async Task<SponsorshipReadDto?> GetSponsorshipRequestAsync(int id)
        => await _http.GetFromJsonAsync<SponsorshipReadDto>($"{_baseUrl}/{id}");

    public async Task<SponsorshipCreateDto?> AddSponsorshipRequestAsync(SponsorshipCreateDto sponsorshipRequest)
    {
        var response = await _http.PostAsJsonAsync(_baseUrl, sponsorshipRequest);
        return await response.Content.ReadFromJsonAsync<SponsorshipCreateDto>();
    }

    public async Task<SponsorshipUpdateDto?> UpdateSponsorshipRequestAsync(SponsorshipUpdateDto sponsorshipRequest)
    {
        var response = await _http.PutAsJsonAsync($"{_baseUrl}/{sponsorshipRequest.SponsorshipId}", sponsorshipRequest);
        return await response.Content.ReadFromJsonAsync<SponsorshipUpdateDto>();
    }

    public async Task<bool> DeleteSponsorshipRequestAsync(int id)
    {
        var response = await _http.DeleteAsync($"{_baseUrl}/{id}");
        return response.IsSuccessStatusCode;
    }
}