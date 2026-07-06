using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesInsight.Application.DTOs;
using SalesInsight.Application.Interfaces;
using SalesInsight.Application.Queries.GetDashboardSummary;

namespace SalesInsight.Application.Handlers.GetDashboardSummary
{
    public class GetDashboardSummaryQueryHandler : IRequestHandler<GetDashboardSummaryQuery, DashboardSummaryDto>
    {
        private readonly IAppDbContext _context;

        public GetDashboardSummaryQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardSummaryDto> Handle(GetDashboardSummaryQuery request, CancellationToken cancellationToken)
        {
            var totalCustomers = await _context.Customers.CountAsync(cancellationToken);
            var totalProducts = await _context.Products.CountAsync(cancellationToken);
            var totalOrders = await _context.Orders.CountAsync(cancellationToken);
            var totalRevenue = await _context.Orders.SumAsync(o => o.TotalAmount, cancellationToken);
            var lowStockProducts = await _context.Products.CountAsync(p => p.StockQuantity < 10, cancellationToken);
            return new DashboardSummaryDto
            {
                TotalCustomers = totalCustomers,
                TotalProducts = totalProducts,
                TotalOrders = totalOrders,
                TotalRevenue = totalRevenue,
                LowStockProducts = lowStockProducts
            };
        }
    }
}
