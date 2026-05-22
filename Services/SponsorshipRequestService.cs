using System.Net.Http.Json;
using LibraryPortalBlazorWebAssemblyApp.Models;
using Microsoft.Extensions.Options;
using Sponsorship.Application.DTOs;

namespace LibraryPortalBlazorWebAssemblyApp.Services;

public class SponsorshipRequestService
{
    private readonly HttpClient _http;
    private readonly string _baseUrl;

    public SponsorshipRequestService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _baseUrl = config["ApiSettings:BaseUrl"] + "/sponsorship-requests";
    }

    public async Task<List<SponsorshipRequestReadDto>> GetSponsorshipRequestsAsync()
        => await _http.GetFromJsonAsync<List<SponsorshipRequestReadDto>>(_baseUrl) ?? new List<SponsorshipRequestReadDto>();

    public async Task<SponsorshipRequestReadDto?> GetSponsorshipRequestAsync(int id)
        => await _http.GetFromJsonAsync<SponsorshipRequestReadDto>($"{_baseUrl}/{id}");

    public async Task<SponsorshipRequestCreateDto?> AddSponsorshipRequestAsync(SponsorshipRequestCreateDto sponsorshipRequest)
    {
        var response = await _http.PostAsJsonAsync(_baseUrl, sponsorshipRequest);
        return await response.Content.ReadFromJsonAsync<SponsorshipRequestCreateDto>();
    }

    public async Task<SponsorshipRequestUpdateDto?> UpdateSponsorshipRequestAsync(SponsorshipRequestUpdateDto sponsorshipRequest)
    {
        var response = await _http.PutAsJsonAsync($"{_baseUrl}/{sponsorshipRequest.SponsorshipId}", sponsorshipRequest);
        return await response.Content.ReadFromJsonAsync<SponsorshipRequestUpdateDto>();
    }

    public async Task<bool> DeleteSponsorshipRequestAsync(int id)
    {
        var response = await _http.DeleteAsync($"{_baseUrl}/{id}");
        return response.IsSuccessStatusCode;
    }
}