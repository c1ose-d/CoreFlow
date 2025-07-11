namespace CoreFlow.Application.DTOs;

public partial class RebootDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public List<RebootCommandDto> RebootCommands { get; set; } = [];

    public List<RebootListDto> RebootLists { get; set; } = [];
}