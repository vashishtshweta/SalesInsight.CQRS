using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesInsight.Application.DTOs.Customer;
using SalesInsight.Application.Interfaces;
using SalesInsight.Application.Queries.Customers;


namespace SalesInsight.Application.Handlers.Customers
{
    public class GetCustomersHandler : IRequestHandler<GetCustomersQuery, List<CustomerResponse>>
    {
        private readonly IAppDbContext _context;

        public GetCustomersHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerResponse>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers
                                    .AsNoTracking()
                                    .OrderBy(c => c.CreatedAt)
                                   .Select(c => new CustomerResponse
                                   {
                                       CustomerId = c.Id,
                                       CompanyName = c.CompanyName,
                                       ContactName = c.ContactName,
                                       Email = c.Email,
                                       CreatedAt = c.CreatedAt
                                   })
                                   .ToListAsync();
        }
    }
}
