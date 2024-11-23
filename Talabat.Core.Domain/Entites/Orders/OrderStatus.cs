using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Domain.Entites.Orders
{
    public enum OrderStatus
    {
        Pending =1,
        Payment=2,
        PaymentFailed=3,
    }
}
