using AutoMapper;
using Maqta.API.Models.Dtos;
using Maqta.API.Repository;
using Maqta.API.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maqta.API.Services
{
    public class ProductService : IProductService
    {
        //there is unit of work but i used repo for simplify unit testing
        //and its one entity no need for transaction of unit of work
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> UpdateProduct(ProductDto productDto)
        {
            ProductDto result =  _productRepository.Update(productDto);
            await _productRepository.SaveChanges();
            return result;
        }

        public async Task<ProductDto> CreateProduct(ProductDto productDto)
        {
            ProductDto result =  _productRepository.Add(productDto);
            await _productRepository.SaveChanges();
            return result;
        }

        public async Task<bool> Delete(int productId)
        {
            ProductDto productDto =await  _productRepository.GetFirstOrDefault(x=>x.ProductId==productId,true);
            bool result =  _productRepository.Remove(productDto);
            await _productRepository.SaveChanges();
            return result;
        }

        public async Task<ProductDto> GetById(int productId)
        {
            ProductDto result = await _productRepository.GetFirstOrDefault(x=>x.ProductId==productId);
            return result;
        }

        public async Task<IEnumerable<ProductDto>> Get()
        {
            IEnumerable<ProductDto> result = await _productRepository.GetAll();
            return result;
        }
    }
}
