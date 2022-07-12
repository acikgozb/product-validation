using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductValidation.API.Filters;
using ProductValidation.Core.Contracts;
using ProductValidation.Core.Repository;
using ProductValidation.Core.Repository.DataGateways;
using ProductValidation.Core.Services;
using ProductValidation.Core.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

builder.Services.AddDbContext<ProductValidationContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        optionsBuilder => optionsBuilder.MigrationsAssembly("ProductValidation.API")
    );
});

builder.Services.AddControllers(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    options.Filters.Add<ModelBindingValidationActionFilter>();
});

builder.Services.AddScoped<IModelValidator, ModelValidator>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductValidationService, ProductValidationService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductDataGateway, ProductDataGateway>();
builder.Services.AddValidatorsFromAssemblyContaining<ProductRequestDtoValidator>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();
app.MapControllers();

app.Run();