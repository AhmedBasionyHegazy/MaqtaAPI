using Maqta.API.DBContexts;
using Maqta.API.Models;
using Maqta.API.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maqta.API.Repository
{
    public interface IProductRepository: IRepository<ApplicationDBContext, Product, ProductDto>
    {
    }
}
