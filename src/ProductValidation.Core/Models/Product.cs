namespace ProductValidation.Core.Models;

public class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Brand { get; set; }

    public string? Barcode { get; set; }

    public string? Description { get; set; }
}