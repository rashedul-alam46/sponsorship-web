using Microsoft.JSInterop;
using Sponsorship.Models;
using System.Text.Json;

public static class SessionStorage
{
    private static IJSRuntime _js;
    private const string KEY = "auth_user";

    public static void Init(IJSRuntime js)
    {
        _js = js;
    }

    public static async Task SetUser(SignInResponseDto user)
    {
        var json = JsonSerializer.Serialize(user);
        await _js.InvokeVoidAsync("sessionStorage.setItem", KEY, json);
    }

    public static async Task<SignInResponseDto?> GetUser()
    {
        var json = await _js.InvokeAsync<string>("sessionStorage.getItem", KEY);

        return string.IsNullOrEmpty(json)
            ? null
            : JsonSerializer.Deserialize<SignInResponseDto>(json);
    }

    public static async Task Clear()
    {
        await _js.InvokeVoidAsync("sessionStorage.removeItem", KEY);
    }
}