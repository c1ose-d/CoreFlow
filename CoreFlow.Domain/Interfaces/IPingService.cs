namespace CoreFlow.Domain.Interfaces;

public interface IPingService
{
    Task<bool> PingAsync(string ipAddress);
}