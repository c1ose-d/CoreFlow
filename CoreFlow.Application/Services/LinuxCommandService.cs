namespace CoreFlow.Application.Services;

public class LinuxCommandService(ILinuxCommandRepository linuxCommandRepository, ILinuxBlockRepository linuxBlockRepository) : ILinuxCommandService
{
    private readonly ILinuxCommandRepository _linuxCommandRepository = linuxCommandRepository;
    private readonly ILinuxBlockRepository _linuxBlockRepository = linuxBlockRepository;

    public async Task<LinuxCommandDto?> GetByIdAsync(Guid id)
    {
        LinuxCommand? linuxCommand = await _linuxCommandRepository.GetByIdAsync(id);

        return linuxCommand == null ? throw new Exception("Linux Command not found.") : new LinuxCommandDto
        {
            Id = linuxCommand.Id,
            Name = linuxCommand.Name,
            Content = linuxCommand.Content,
            BlockId = linuxCommand.BlockId,
            LinuxBlockDto = linuxCommand.Block == null ? null : new LinuxBlockDto
            {
                Id = linuxCommand.Block.Id,
                Name = linuxCommand.Block.Name
            }
        };
    }

    public async Task<IReadOnlyCollection<LinuxCommandDto>> GetAllAsync()
    {
        IReadOnlyCollection<LinuxCommand> linuxCommands = await _linuxCommandRepository.GetAllAsync();

        return [.. linuxCommands.Select(linuxCommand => new LinuxCommandDto
        {
            Id = linuxCommand.Id,
            Name = linuxCommand.Name,
            Content = linuxCommand.Content,
            BlockId = linuxCommand.BlockId,
            LinuxBlockDto = linuxCommand.Block == null ? null : new LinuxBlockDto
            {
                Id = linuxCommand.Block.Id,
                Name = linuxCommand.Block.Name
            }
        })];
    }

    public async Task AddAsync(LinuxCommandDto linuxCommandDto)
    {
        LinuxBlock? linuxBlock = await _linuxBlockRepository.GetByIdAsync(linuxCommandDto.BlockId) ?? throw new Exception("Linux Block not found.");

        if (linuxCommandDto.Name.Length > 200)
        {
            throw new Exception("The Name must be no longer than 200 characters.");
        }

        LinuxCommand linuxCommand = new()
        {
            Name = linuxCommandDto.Name,
            Content = linuxCommandDto.Content,
            Block = linuxBlock
        };

        await _linuxCommandRepository.AddAsync(linuxCommand);
    }

    public async Task EditAsync(LinuxCommandDto linuxCommandDto)
    {
        LinuxCommand? linuxCommand = await _linuxCommandRepository.GetByIdAsync(linuxCommandDto.Id) ?? throw new Exception("Linux Command not found.");

        if (linuxCommandDto.Name.Length > 200)
        {
            throw new Exception("The Name must be no longer than 200 characters.");
        }

        LinuxBlock? linuxBlock = await _linuxBlockRepository.GetByIdAsync(linuxCommandDto.BlockId) ?? throw new Exception("Linux Block not found.");

        linuxCommand.Name = linuxCommandDto.Name;
        linuxCommand.Content = linuxCommandDto.Content;
        linuxCommand.Block = linuxBlock;

        await _linuxCommandRepository.EditAsync(linuxCommand);
    }

    public async Task DeleteAsync(Guid id)
    {
        _ = await _linuxCommandRepository.GetByIdAsync(id) ?? throw new Exception("Linux Command not found.");

        await _linuxCommandRepository.DeleteAsync(id);
    }
}