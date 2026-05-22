using System.Net.Http.Json;
using Sponsorship.Application.DTOs;

namespace Sponsorship.Services;

public class DropdownService
{
    private readonly HttpClient _http;
    private readonly string _baseUrl;

    public DropdownService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _baseUrl = config["ApiSettings:BaseUrl"] + "/dropdown";
    }

    public async Task<List<DropdownItem>> GetCategoriesAsync()
        => await _http.GetFromJsonAsync<List<DropdownItem>>($"{_baseUrl}/departments") ?? new List<DropdownItem>();

    public async Task<List<DropdownItem>> GetAuthorsAsync()
        => await _http.GetFromJsonAsync<List<DropdownItem>>($"{_baseUrl}/sponsorshiptypes") ?? new List<DropdownItem>();


}