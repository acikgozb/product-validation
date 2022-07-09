using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProductValidation.API.Filters;
using ProductValidation.Core.Contracts;
using ProductValidation.Core.Dtos;
using ProductValidation.Core.Services;
using ProductValidation.Core.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

builder.Services.AddControllers(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

builder.Services.AddScoped<IModelValidator, ProductValidationService>();
builder.Services.AddScoped<IValidator<ProductRequestDto>, ProductRequestDtoValidator>();


var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();
app.MapControllers();

app.Run();