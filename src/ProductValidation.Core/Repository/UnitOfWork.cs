using ProductValidation.Core.Contracts;

namespace ProductValidation.Core.Repository;

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