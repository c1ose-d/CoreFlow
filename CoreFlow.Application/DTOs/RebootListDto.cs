namespace CoreFlow.Application.DTOs;

public partial class RebootListDto
{
    public RebootDto RebootDto { get; set; } = null!;
    public ServerDto? ServerDto { get; set; }
    public ServerBlockDto? ServerBlockDto { get; set; }
}