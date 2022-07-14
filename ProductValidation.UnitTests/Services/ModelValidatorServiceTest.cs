using FluentValidation;
using FluentValidation.Results;
using Moq;
using ProductValidation.Core.Exceptions;
using ProductValidation.Core.Models;
using ProductValidation.Core.Services;
using ProductValidation.UnitTests.Util.TestBase;

namespace ProductValidation.UnitTests.Services;

public class ModelValidatorServiceTest : TestBase<ModelValidatorService>
{
    private readonly Product _mockProductToValidate = new();
    
    [Fact]
    public void Validate_Should_Throw_If_Required_Validator_Is_Not_Injected()
    {
        MockFor<IServiceProvider>()
            .Setup(dep => dep.GetService(It.IsAny<Type>()))
            .Returns(null);
        
        Assert.Throws<NullValidatorException<Product>>(() => ClassUnderTest.Validate(_mockProductToValidate));
    }

    [Fact]
    public void Validate_Should_Return_Field_Validation_Results_As_List()
    {
        var mockValidationResult = new ValidationResult
        {
            Errors = new List<ValidationFailure>()
            {
                new("MockProperty", "MockErrorMessage")
            }
        };

        var mockProductValidator = MockFor<IValidator<Product>>();
        
        mockProductValidator.Setup(dep => dep.Validate(It.IsAny<Product>()))
            .Returns(mockValidationResult);

        MockFor<IServiceProvider>()
            .Setup(dep => dep.GetService(It.IsAny<Type>()))
            .Returns(mockProductValidator.Object);

        var testResult = ClassUnderTest.Validate(_mockProductToValidate);
        
        MockFor<IValidator<Product>>()
            .Verify(dep => dep.Validate(It.IsAny<Product>()), Times.Once);
        
        Assert.Equal(testResult[0].FieldName, mockValidationResult.Errors[0].PropertyName);
        Assert.Equal(testResult[0].ErrorMessage, mockValidationResult.Errors[0].ErrorMessage);
    }
    
    [Fact]
    public void ValidateAsync_Should_Throw_If_Required_Validator_Is_Not_Injected()
    {
        MockFor<IServiceProvider>()
            .Setup(dep => dep.GetService(It.IsAny<Type>()))
            .Returns(null);
        
        Assert.ThrowsAsync<NullValidatorException<Product>>(async () => await ClassUnderTest.ValidateAsync(_mockProductToValidate));
    }

    [Fact]
    public async Task ValidateAsync_Should_Return_Field_Validation_Results_As_List()
    {
        var mockValidationResult = new ValidationResult
        {
            Errors = new List<ValidationFailure>()
            {
                new("MockProperty", "MockErrorMessage")
            }
        };

        var mockProductValidator = MockFor<IValidator<Product>>();

        mockProductValidator.Setup(dep => dep.ValidateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()).Result).Returns(mockValidationResult);

        MockFor<IServiceProvider>()
            .Setup(dep => dep.GetService(It.IsAny<Type>()))
            .Returns(mockProductValidator.Object);

        var testResult = await ClassUnderTest.ValidateAsync(_mockProductToValidate);
        
        MockFor<IValidator<Product>>()
            .Verify(dep => dep.ValidateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once);
        
        Assert.Equal(testResult[0].FieldName, mockValidationResult.Errors[0].PropertyName);
        Assert.Equal(testResult[0].ErrorMessage, mockValidationResult.Errors[0].ErrorMessage);
    }
}