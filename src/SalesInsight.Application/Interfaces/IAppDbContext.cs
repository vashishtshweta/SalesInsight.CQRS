using Microsoft.EntityFrameworkCore;
using SalesInsight.Domain.Entities;

namespace SalesInsight.Application.Interfaces
{
    public interface IAppDbContext
    {

         DbSet<Customer> Customers { get; } 
         DbSet<Product> Products { get; }
         DbSet<Order> Orders { get; }
          DbSet<OrderItem> OrderItems { get; }

         Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
