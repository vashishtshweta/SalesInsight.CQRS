using MediatR;
using SalesInsight.Application.DTOs.Customer;
namespace SalesInsight.Application.Queries.Customers
{
    public record GetCustomerByIdQuery(Guid CustomerId) : IRequest<CustomerResponse?>;
}
