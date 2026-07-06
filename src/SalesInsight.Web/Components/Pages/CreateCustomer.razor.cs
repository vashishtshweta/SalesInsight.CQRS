using Microsoft.AspNetCore.Components;
using SalesInsight.Application.DTOs;
using SalesInsight.Application.DTOs.Customer;
using SalesInsight.Web.ApiClients;
using System.ComponentModel.DataAnnotations;

namespace SalesInsight.Web.Components.Pages;

public partial class CreateCustomer
{
    private CreateCustomerViewModel _model = new();

    private bool _isSaving;

    private string? _errorMessage;

    [Inject]
    private CustomerApiClient ApiClient { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    private async Task CreateCustomerAsync()
    {
        _isSaving = true;
        _errorMessage = null;

        try
        {
            var request = new CreateCustomerRequest
            (
                _model.CompanyName,
                _model.ContactName,
                _model.Email
            );

            await ApiClient.CreateCustomerAsync(request);

            NavigationManager.NavigateTo("/customers");
        }
        catch (Exception ex)
        {
            _errorMessage = $"Failed to create customer: {ex.Message}";
        }
        finally
        {
            _isSaving = false;
        }
    }

    private sealed class CreateCustomerViewModel
    {
        [Required]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        public string ContactName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}