using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInsight.Domain.Enums
{
    public enum OrderStatus
    {
        Draft = 1,
        Submitted = 2,
        Processing = 3,
        Completed = 4,
        Cancelled = 5
    }
}
