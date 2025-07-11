namespace CoreFlow.Application.DTOs;

public class DveIdDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Content { get; set; } = null!;
    public Guid BlockId { get; set; }

    public DveBlockDto? DveBlockDto { get; set; }
}