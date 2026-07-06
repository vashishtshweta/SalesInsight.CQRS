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

        public async Task<List<CustomerResponse>> GetCustomersAsync(CancellationToken cancellationToken = default)
        {
            var customers = await _httpClient.GetFromJsonAsync<List<CustomerResponse>>( ApiRoutes.CustomersBase, cancellationToken);

            return customers ?? new List<CustomerResponse>();
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
