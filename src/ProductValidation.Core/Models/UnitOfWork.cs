using ProductValidation.Core.Contracts;
using ProductValidation.Core.Repository;

namespace ProductValidation.Core.Models;

public class UnitOfWork : IUnitOfWork
{
    private readonly ProductValidationContext _dbContext;

    public UnitOfWork(ProductValidationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}