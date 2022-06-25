using Maqta.API.Models.Dtos;
using Maqta.API.Repository;
using Maqta.API.Services;
using Maqta.API.UnitOfWorks;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Maqta.ApI.Moq
{
    //there is alot of test cases and this class to prove that i am able to prictice in moq only
    //because there is not enough time and i am started this solution from 0
    public class ProductServiceTests
    {
        private readonly ProductService _sut;
        private readonly Mock<IUnitOfWork> _uow = new Mock<IUnitOfWork>();
        private readonly Mock<IProductRepository> _productRepoMock = new Mock<IProductRepository>();

        public ProductServiceTests()
        {
            _sut = new ProductService(_productRepoMock.Object);
        }

        [Fact]
        public async Task GetById_ShouldReturnProduct_WhenExist()
        {
            //Arrange
            var productId = 1;
            var name = "Car";
            var description = "good car for you";
            var price = 15;
            var productDto = new ProductDto
            {
                ProductId=productId,
                Description= description,
                Name= name,
                Price= price
            };
            _productRepoMock.Setup(x => x.GetFirstOrDefault(y => y.ProductId == productId, false, null)).ReturnsAsync(productDto);
            //Act
            var product =await _sut.GetById(productId);
            //Assert
            Assert.Equal(productId, product.ProductId);
            Assert.Equal(name, product.Name);
            Assert.Equal(description, product.Description);
            Assert.Equal(price, product.Price);
        }

        [Fact]
        public async Task GetById_ShouldReturnNothing_WhenDoesNotExist()
        {
            //Arrange
            _productRepoMock.Setup(x => x.GetFirstOrDefault(y => y.ProductId == It.IsAny<int>(), false, null)).ReturnsAsync(()=>null);
            //Act
            var product = await _sut.GetById(0);
            //Assert
            Assert.Null(product);
        }
    }
}
