using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Products;

namespace Talabat.Core.Application.Abstraction
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
    }
}
