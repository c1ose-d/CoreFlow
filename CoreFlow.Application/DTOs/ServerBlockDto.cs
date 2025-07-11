namespace CoreFlow.Application.DTOs;

public partial class ServerBlockDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    
    public IReadOnlyCollection<ServerDto> Servers { get; set; } = [];
}