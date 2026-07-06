
namespace SalesInsight.Application.DTOs.Customer
{
    public record CreateCustomerRequest(
    string CompanyName,
    string ContactName,
    string Email);
}
