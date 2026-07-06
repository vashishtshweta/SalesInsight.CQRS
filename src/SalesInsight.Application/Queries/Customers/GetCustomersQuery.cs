using MediatR;
using SalesInsight.Application.DTOs.Common;
using SalesInsight.Application.DTOs.Customer;

namespace SalesInsight.Application.Queries.Customers
{
    public record GetCustomersQuery(
     int PageNumber = 1,
     int PageSize = 10,
     string? SearchTerm = null
 ) : IRequest<PagedResult<CustomerResponse>>;
}
