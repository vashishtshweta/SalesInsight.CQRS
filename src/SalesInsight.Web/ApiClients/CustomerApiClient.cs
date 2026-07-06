using SalesInsight.Application.DTOs.Common;
using SalesInsight.Application.DTOs.Customer;
using System.Net;

namespace SalesInsight.Web.ApiClients
{
    public class CustomerApiClient
    {
        private readonly HttpClient _httpClient;

        public CustomerApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateCustomerAsync(CreateCustomerRequest request, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.CustomersBase, request, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public async Task<PagedResult<CustomerResponse>> GetCustomersAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = null,
            CancellationToken cancellationToken = default)
        {
            var url = $"{ApiRoutes.CustomersBase}?pageNumber={pageNumber}&pageSize={pageSize}";

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                url += $"&searchTerm={Uri.EscapeDataString(searchTerm)}";
            }
            var customers = await _httpClient.GetFromJsonAsync<PagedResult<CustomerResponse>>( url, cancellationToken);

            return customers ?? new PagedResult<CustomerResponse>();
        }

        public async Task<CustomerResponse?> GetCustomerAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"{ApiRoutes.CustomersBase}/{customerId}", cancellationToken);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<CustomerResponse>( cancellationToken: cancellationToken);
        }
    }
}
