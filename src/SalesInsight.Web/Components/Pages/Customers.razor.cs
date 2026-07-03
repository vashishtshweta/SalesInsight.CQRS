using MediatR;
using Microsoft.AspNetCore.Components;
using SalesInsight.Application.Commands.Customers;
using SalesInsight.Application.DTOs.Customer;
using SalesInsight.Application.Queries.Customers;
using System.ComponentModel.DataAnnotations;

namespace SalesInsight.Web.Pages;

public partial class Customers
{
    private CreateCustomerRequest? _model;
    private List<CustomerResponse>? _customers;

    private CustomerResponse? _createdCustomer;

    private bool _isSaving;

    private string? _message;

    private string? _error;

    [Inject]
    private ISender Sender { get; set; } = default!;
}