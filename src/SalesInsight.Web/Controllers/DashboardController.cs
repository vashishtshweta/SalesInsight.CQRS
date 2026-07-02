using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesInsight.Application.DTOs;
using SalesInsight.Application.Queries.GetDashboardSummary;

namespace SalesInsight.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILogger<DashboardController> _logger;

    public DashboardController(IMediator mediator, ILogger<DashboardController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("summary")]
    public async Task<ActionResult<DashboardSummaryDto>> GetDashboardSummaryAsync(CancellationToken cancellationToken) { 
    
        var result = await _mediator.Send(new GetDashboardSummaryQuery(), cancellationToken);
        return Ok(result);
    }
}
