using MediatR;
using SalesInsight.Application.Commands.Customers;
using SalesInsight.Application.Interfaces;
using SalesInsight.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInsight.Application.Handlers.Customers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand,Guid>
    {
        private readonly IAppDbContext _context;

        public CreateCustomerCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                CompanyName = request.CompanyName,
                ContactName = request.ContactName,
                Email = request.Email,
                CreatedAt = DateTime.UtcNow
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync(cancellationToken);

            return customer.Id;
        }
    }
}
