using Maqta.API.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maqta.API.Services
{
    public interface IProductService
    {
        Task<ProductDto> UpdateProduct(ProductDto productDto);
        Task<ProductDto> CreateProduct(ProductDto productDto);
        Task<IEnumerable<ProductDto>> Get();
        Task<ProductDto> GetById(int productId);
        Task<bool> Delete(int productId);
    }
}
