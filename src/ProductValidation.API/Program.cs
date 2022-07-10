using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProductValidation.API.Filters;
using ProductValidation.Core.Contracts;
using ProductValidation.Core.Services;
using ProductValidation.Core.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

builder.Services.AddControllers(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    options.Filters.Add<ModelBindingValidationActionFilter>();
});

builder.Services.AddScoped<IModelValidator, ModelValidator>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductValidationService, ProductValidationService>();
builder.Services.AddValidatorsFromAssemblyContaining<ProductRequestDtoValidator>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();
app.MapControllers();

app.Run();