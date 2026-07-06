
namespace SalesInsight.Application.DTOs.Customer
{
    public class CustomerResponse
    {
        public Guid CustomerId { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string ContactName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
