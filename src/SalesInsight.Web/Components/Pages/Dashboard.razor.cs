using Microsoft.AspNetCore.Components;
using SalesInsight.Application.DTOs;
using SalesInsight.Web.ApiClients;

namespace SalesInsight.Web.Components.Pages;

public partial class Dashboard
{
    private DashboardSummaryDto? _summary;

    [Inject]
    private DashboardApiClient ApiClient { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _summary = await ApiClient.GetSummaryAsync(CancellationToken.None);
    }
}