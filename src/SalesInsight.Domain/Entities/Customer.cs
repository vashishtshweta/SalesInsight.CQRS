using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInsight.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string ContactName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Order> Orders { get; set; } = []; //can i have list what benefit of having collection over list erhe 
    }
}
