using MediatR;
using SalesInsight.Application.DTOs.Customer;

namespace SalesInsight.Application.Queries.Customers
{
    public class GetCustomersQuery : IRequest<List<CustomerResponse>>;
}
