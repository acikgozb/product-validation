using Moq;
using ProductValidation.Core.Contracts;
using ProductValidation.Core.Models;
using ProductValidation.Core.Models.Dtos;
using ProductValidation.Core.Services;
using ProductValidation.UnitTests.Util.TestBase;

namespace ProductValidation.UnitTests.Services;

public class ProductServiceTests : TestBase<ProductService>
{
    [Fact]
    public async Task GetProductsAsync_Returns_List_Of_All_Products()
    {
        var mockListOfAllProducts = new List<Product>();

        MockFor<IProductDataGateway>()
            .Setup(dep => dep.GetProductsAsync().Result).Returns(mockListOfAllProducts);

        var testResult = await ClassUnderTest.GetProductsAsync();

        MockFor<IProductDataGateway>()
            .Verify(dep => dep.GetProductsAsync(), Times.Once);
        Assert.StrictEqual(mockListOfAllProducts, testResult);
    }

    [Fact]
    public async Task GetProductByIdAsync_Returns_Product_With_Requested_Id()
    {
        var requestedProductId = 1;
        var mockProduct = new Product
        {
            Id = requestedProductId
        };

        MockFor<IProductDataGateway>()
            .Setup(dep => dep.GetProductByIdAsync(It.IsAny<int>()).Result).Returns(mockProduct);

        var testResult = await ClassUnderTest.GetProductByIdAsync(requestedProductId);

        MockFor<IProductDataGateway>().Verify(dep => dep.GetProductByIdAsync(It.IsAny<int>()), Times.Once);
        Assert.StrictEqual(mockProduct, testResult);
    }

    [Fact]
    public async Task AddProductAsync_Returns_Validation_Errors_For_Invalid_Product()
    {
        var mockedProductValidationResult = new List<FieldValidationResult>
        {
            new FieldValidationResult
            {
                FieldName = "mock field name",
                ErrorMessage = "mock error message"
            }
        };

        MockFor<IModelValidatorService>()
            .Setup(dep => dep.ValidateAsync(It.IsAny<Product>()).Result)
            .Returns(mockedProductValidationResult);

        var testResult = await ClassUnderTest.AddProductAsync(new ProductRequestDto());

        Assert.StrictEqual(mockedProductValidationResult, testResult);

        MockFor<IModelValidatorService>()
            .Verify(dep => dep.ValidateAsync(It.IsAny<Product>()), Times.Once);

        MockFor<IProductDataGateway>()
            .Verify(dep => dep.AddProduct(It.IsAny<Product>()), Times.Never);

        MockFor<IUnitOfWork>()
            .Verify(dep => dep.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task AddProductAsync_Returns_Added_Product_If_It_Is_Valid()
    {
        var mockAddedProduct = new Product { Id = 1 };
        var validProductValidationErrors = new List<FieldValidationResult>();

        MockFor<IModelValidatorService>()
            .Setup(dep => dep.ValidateAsync(It.IsAny<Product>()).Result)
            .Returns(validProductValidationErrors);

        MockFor<IProductDataGateway>()
            .Setup(dep => dep.AddProduct(It.IsAny<Product>()))
            .Returns(mockAddedProduct);

        MockFor<IUnitOfWork>()
            .Setup(dep => dep.SaveChangesAsync())
            .Returns(Task.FromResult(true));

        var testResult = await ClassUnderTest.AddProductAsync(new ProductRequestDto());

        Assert.StrictEqual(mockAddedProduct, testResult);

        MockFor<IModelValidatorService>()
            .Verify(dep => dep.ValidateAsync(It.IsAny<Product>()), Times.Once);

        MockFor<IProductDataGateway>()
            .Verify(dep => dep.AddProduct(It.IsAny<Product>()), Times.Once);

        MockFor<IUnitOfWork>()
            .Verify(dep => dep.SaveChangesAsync(), Times.Once);
    }
}