using Moq;
using ProductValidation.Core.Contracts;
using ProductValidation.Core.Models;
using ProductValidation.Core.Services;
using ProductValidation.UnitTests.Util.TestBase;

namespace ProductValidation.UnitTests.Services;

public class ProductServiceTests : TestBase<ProductService>
{
    [Fact]
    public async Task GetProductsAsync_Should_Call_Product_Data_Gateway()
    {
        MockFor<IProductDataGateway>()
            .Setup(dep => dep.GetProductsAsync().Result).Returns(new List<Product>());

        await ClassUnderTest.GetProductsAsync();

        MockFor<IProductDataGateway>()
            .Verify(dep => dep.GetProductsAsync(), Times.Once);
    }
}