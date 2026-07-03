using MediatR;
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
            var customer = await _context.Customers.FindAsync(new object[] { request }, cancellationToken);
            if (customer == null)
            {
                _logger.LogWarning(  "Customer with ID {CustomerId} was not found.",  request.CustomerId);
                return null;
            }
            return new CustomerResponse
            {
                CustomerId = customer.Id,
                CompanyName = customer.CompanyName,
                ContactName = customer.ContactName,
                Email = customer.Email,
                CreatedAt = customer.CreatedAt
            };
        }
    }
}
