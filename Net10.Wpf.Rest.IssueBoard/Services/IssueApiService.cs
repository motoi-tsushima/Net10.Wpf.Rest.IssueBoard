using Shared.Rest.IssueBoard;
using System.Net.Http;
using System.Net.Http.Json;

namespace Net10.Wpf.Rest.IssueBoard.Services;

public class IssueApiService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://localhost:7270/api/issues";

    public IssueApiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<IssueDto>> GetAllIssuesAsync()
    {
        var response = await _httpClient.GetAsync(BaseUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<IssueDto>>() ?? new List<IssueDto>();
    }

    public async Task<IssueDto?> GetIssueAsync(int id)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IssueDto>();
        }
        return null;
    }

    public async Task<IssueDto?> CreateIssueAsync(CreateIssueDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseUrl, dto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IssueDto>();
    }

    public async Task UpdateIssueAsync(int id, UpdateIssueDto dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", dto);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteIssueAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
        response.EnsureSuccessStatusCode();
    }
}
