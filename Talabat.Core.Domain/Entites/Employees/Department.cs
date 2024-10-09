using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Domain.Entites.Employees
{
    public class Department:BaseAuditableEntity<int>
    {
        public required string Name { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
