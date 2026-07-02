using SalesInsight.Application.DTOs;

namespace SalesInsight.Web.ApiClients;

public class DashboardApiClient
{
    private readonly HttpClient _httpClient;

    public DashboardApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<DashboardSummaryDto?> GetSummaryAsync(CancellationToken cancellationToken = default)
    {
        var result = await _httpClient.GetFromJsonAsync<DashboardSummaryDto>(
       ApiRoutes.DashboardSummary,
       cancellationToken);

        return result ?? new DashboardSummaryDto();
    }
}
