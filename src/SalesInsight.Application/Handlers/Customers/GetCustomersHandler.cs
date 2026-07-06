using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesInsight.Application.DTOs.Common;
using SalesInsight.Application.DTOs.Customer;
using SalesInsight.Application.Interfaces;
using SalesInsight.Application.Queries.Customers;


namespace SalesInsight.Application.Handlers.Customers
{
    public class GetCustomersHandler : IRequestHandler<GetCustomersQuery, PagedResult<CustomerResponse>>
    {
        private readonly IAppDbContext _context;

        public GetCustomersHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<CustomerResponse>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
            var pageSize = request.PageSize is < 1 or > 100 ? 10 : request.PageSize;
            var query = _context.Customers.AsNoTracking();

            if(!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.Trim().ToLower();
                query = query.Where(c => c.CompanyName.ToLower().Contains(searchTerm) ||
                                         c.ContactName.ToLower().Contains(searchTerm) ||
                                         c.Email.ToLower().Contains(searchTerm));
            }

            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .OrderBy(c => c.CompanyName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CustomerResponse
                {
                    CustomerId = c.Id,
                    CompanyName = c.CompanyName,
                    ContactName = c.ContactName,
                    Email = c.Email,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<CustomerResponse>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
           
    }
}
