using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesInsight.Application.Commands.Customers;
using SalesInsight.Application.DTOs.Customer;
using SalesInsight.Application.Queries.Customers;

namespace SalesInsight.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;
    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerResponse>>> GetCustomersAsync( [FromQuery] int pageNumber =1,
        [FromQuery] int pageSize =10,
        [FromQuery] string? searchTerm = null, CancellationToken cancellationToken = default)
    {
        var customers = await _mediator.Send(new GetCustomersQuery(pageNumber, pageSize, searchTerm ),cancellationToken);
        return Ok(customers);
    
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CustomerResponse>> GetCustomerById(Guid id, CancellationToken cancellationToken)
    {
        var customer = await _mediator.Send(new GetCustomerByIdQuery(id), cancellationToken);
        if(customer == null) {
             return NotFound();
        }
        return Ok(customer);
    }


    [HttpPost]

    public async Task<ActionResult> CreateCustomer(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCustomerCommand(request.CompanyName, request.ContactName, request.Email);
        var customerId = await _mediator.Send(command,cancellationToken);
        return CreatedAtAction(nameof(GetCustomerById), new { id = customerId }, customerId);
       // return customerId;
    }

}
