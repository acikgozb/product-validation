using Microsoft.EntityFrameworkCore;
using ProductValidation.Core.Contracts;
using ProductValidation.Core.Models;

namespace ProductValidation.Core.Repository.DataGateways;

public class ProductDataGateway : IProductDataGateway
{
    private readonly ProductValidationContext _dbContext;

    public ProductDataGateway(ProductValidationContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Generates a database query to find out whether there is a Product created before with given barcode. 
    /// </summary>
    /// <param name="barcode">(<c>string</c>) unique barcode.</param>
    /// <returns>
    /// <c>true</c> - if there is a product exist by given barcode.<br />
    /// <c>false</c> - if given barcode is truly unique.<br />
    /// </returns>
    public Task<bool> DoesProductExistByBarcodeAsync(string barcode)
    {
        return _dbContext.Products.AnyAsync(product => product.Barcode == barcode);
    }

    /// <summary>
    /// Generates a database query to add given product.
    /// </summary>
    /// <param name="product">(<c>Product</c>) validated Product.</param>
    /// <returns>
    /// <c>Product</c> - Added Product that has given a unique Id.<br />
    /// </returns>
    public Product AddProduct(Product product)
    {
        var productEntry = _dbContext.Products.Add(product);
        return productEntry.Entity;
    }

    /// <summary>
    /// Generates a database query to get the list of all products.
    /// </summary>
    /// <returns>
    /// Product <c>List</c> - a list of all products.<br />
    /// </returns>
    public Task<List<Product>> GetProductsAsync()
    {
        return _dbContext.Products.ToListAsync();
    }

    /// <summary>
    /// Generates a database query to get a product by given id.
    /// </summary>
    /// <param name="Id">(<c>int</c>) Requested Product's Id.</param>
    /// <returns>
    /// <c>Product</c> - the corresponding Product for given Id.<br/>
    /// <c>null</c> - if there isn't any Product with given Id.
    /// </returns>
    public Task<Product?> GetProductByIdAsync(int id)
    {
        return _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
    }
}