using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Application.Common.Exceptions
{
    public class UnAutorizedException:ApplicationException
    {
        public UnAutorizedException(string message)
            :base(message)
        {
            
        }
    }
}
