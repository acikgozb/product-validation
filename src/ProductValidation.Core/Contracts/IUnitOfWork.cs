namespace ProductValidation.Core.Contracts;

public interface IUnitOfWork
{
    public Task SaveChangesAsync();
}