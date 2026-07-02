using MediatR;
using SalesInsight.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInsight.Application.Queries.GetDashboardSummary
{
    public record GetDashboardSummaryQuery : IRequest<DashboardSummaryDto>;
}
