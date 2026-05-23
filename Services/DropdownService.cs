using System.Net.Http.Json;
using Sponsorship.Models;

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
    {
        var response = await _http.GetFromJsonAsync<ApiResponse<List<DropdownItem>>>($"{_baseUrl}/departments");
        return response?.Data ?? new List<DropdownItem>();
    }

    public async Task<List<DropdownItem>> GetAuthorsAsync()
    {
        var response = await _http.GetFromJsonAsync<ApiResponse<List<DropdownItem>>>($"{_baseUrl}/sponsorshiptypes");
        return response?.Data ?? new List<DropdownItem>();
    }


}