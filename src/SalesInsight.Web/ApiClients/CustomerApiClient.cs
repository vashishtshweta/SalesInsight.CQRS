using SalesInsight.Application.DTOs.Customer;

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
            var response = await _httpClient.GetAsync(ApiRoutes.CustomersBase, cancellationToken);
            response.EnsureSuccessStatusCode();
            var customers = await response.Content.ReadFromJsonAsync<List<CustomerResponse>>(cancellationToken: cancellationToken);
            return customers ?? new List<CustomerResponse>();
        }

        public async Task<CustomerResponse?> GetCustomerAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"{ApiRoutes.CustomersBase}/{customerId}", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CustomerResponse>(cancellationToken: cancellationToken);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                response.EnsureSuccessStatusCode();
                return null; // This line will never be reached due to the above line throwing an exception.
            }
        }
    }
}
