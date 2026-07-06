using Microsoft.AspNetCore.Components;
using SalesInsight.Application.DTOs.Customer;
using SalesInsight.Web.ApiClients;

namespace SalesInsight.Web.Components.Pages;

public partial class Customers
{
    private List<CustomerResponse>? _customers;

    [Inject]
    private CustomerApiClient ApiClient { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _customers = await ApiClient.GetCustomersAsync();
    }
}