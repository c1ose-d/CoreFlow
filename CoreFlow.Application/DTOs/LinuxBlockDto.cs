namespace CoreFlow.Application.DTOs;

public class LinuxBlockDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public IReadOnlyCollection<LinuxCommandDto> LinuxCommands { get; set; } = [];
}