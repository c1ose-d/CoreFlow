namespace CoreFlow.Infrastructure.Services;

public class IcmpPingService : IPingService
{
    public async Task<bool> PingAsync(string ipAddress)
    {
        using Ping ping = new();
        PingReply pingReply = await ping.SendPingAsync(ipAddress, 1000);
        return pingReply.Status == IPStatus.Success;
    }
}