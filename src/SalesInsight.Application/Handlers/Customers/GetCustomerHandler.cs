using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SalesInsight.Application.DTOs.Customer;
using SalesInsight.Application.Interfaces;
using SalesInsight.Application.Queries.Customers;

namespace SalesInsight.Application.Handlers.Customers
{
    public class GetCustomerHandler : IRequestHandler<GetCustomerByIdQuery,CustomerResponse?>
    {
        private readonly ILogger<GetCustomerHandler> _logger;
        private readonly IAppDbContext _context;

        public GetCustomerHandler(ILogger<GetCustomerHandler> logger, IAppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<CustomerResponse?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers
                            .AsNoTracking()
                            .Where(c => c.Id == request.CustomerId)
                            .Select(c => new CustomerResponse
                            {
                                CustomerId = c.Id,
                                CompanyName = c.CompanyName,
                                ContactName = c.ContactName,
                                Email = c.Email,
                                CreatedAt = c.CreatedAt
                            })
                            .FirstOrDefaultAsync(cancellationToken);

        }
    }
}
