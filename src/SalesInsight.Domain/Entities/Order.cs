using SalesInsight.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInsight.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; } = default!;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public OrderStatus Status { get; set; } = OrderStatus.Draft;

        public decimal TotalAmount { get; set; }

        public ICollection<OrderItem> Items { get; set; } = [];
    }
}
