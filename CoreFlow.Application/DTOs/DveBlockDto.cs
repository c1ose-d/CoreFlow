namespace CoreFlow.Application.DTOs;

public class DveBlockDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public IReadOnlyCollection<DveIdDto> DveIds { get; set; } = [];
}