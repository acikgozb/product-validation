using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Product Validation API",
        Description =
            "A Web API that showcases cross field and async validations on a simplified Product model by using Fluent Validation .NET library.",
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
    options.AssumeDefaultVersionWhenUnspecified = true;
});

builder.Services.AddHealthChecks();

builder.Services.AddScoped<IModelValidatorService, ModelValidatorService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductDataGateway, ProductDataGateway>();
builder.Services.AddScoped<IBarcodeLengthDataGateway, BarcodeLengthDataGateway>();
builder.Services.AddScoped<IBannedWordsDataGateway, BannedWordsDataGateway>();
builder.Services.AddValidatorsFromAssemblyContaining<ProductRequestDtoValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductValidationContext>();
    var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
    if (pendingMigrations.Any())
    {
        dbContext.Database.Migrate();
    }
}

app.MapHealthChecks("/health");

app.UseHttpsRedirection();

app.UseRouting();
app.MapControllers();

app.Run();