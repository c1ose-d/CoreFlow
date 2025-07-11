namespace CoreFlow.Application.DTOs;

public class LinuxCommandDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Content { get; set; } = null!;
    public Guid BlockId { get; set; }

    public LinuxBlockDto? LinuxBlockDto { get; set; }
}