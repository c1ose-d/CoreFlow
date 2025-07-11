namespace CoreFlow.Application.DTOs;

public partial class RebootCommandDto
{
    public string CommandText { get; set; } = null!;
    public int ExecutionOrder { get; set; }

    public RebootDto RebootDto { get; set; } = null!;
}