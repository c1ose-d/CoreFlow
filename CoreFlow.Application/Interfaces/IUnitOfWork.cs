namespace CoreFlow.Application.Interfaces;

public interface IUnitOfWork
{
    Task<int> CompleteAsync(CancellationToken cancellationToken = default);
}