using MediatR;

namespace SalesInsight.Application.Commands.Customers
{
    public record CreateCustomerCommand(
        string CompanyName,
        string ContactName,
        string Email
    ) : IRequest<Guid>;
}
