using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInsight.Application.Commands.Customers;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .Must(name => !string.IsNullOrWhiteSpace(name))
            .WithMessage("Company name is required")
            .MaximumLength(100);
        RuleFor(x => x.ContactName)
            .NotEmpty()
            .Must(name => !string.IsNullOrWhiteSpace(name))
            .WithMessage("Contact name is required")
            .MaximumLength(150);
        RuleFor(x => x.Email)
            .NotEmpty()
            .Must(email => !string.IsNullOrWhiteSpace(email))
            .WithMessage("Email is required")
            .EmailAddress()
            .MaximumLength(250);
    }

}
