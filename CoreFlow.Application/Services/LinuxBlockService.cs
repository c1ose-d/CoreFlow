namespace CoreFlow.Application.Services;

public class LinuxBlockService(ILinuxBlockRepository linuxBlockRepository) : ILinuxBlockService
{
    private readonly ILinuxBlockRepository _linuxBlockRepository = linuxBlockRepository;

    public async Task<LinuxBlockDto?> GetByIdAsync(Guid id)
    {
        LinuxBlock? linuxBlock = await _linuxBlockRepository.GetByIdAsync(id);

        return linuxBlock == null ? throw new Exception("Linux Block not found.") : new LinuxBlockDto
        {
            Id = linuxBlock.Id,
            Name = linuxBlock.Name,
            LinuxCommands = linuxBlock.LinuxCommands.Select(linuxCommand => new LinuxCommandDto
            {
                Id = linuxCommand.Id,
                Name = linuxCommand.Name,
                Content = linuxCommand.Content
            }).ToList().AsReadOnly()
        };
    }

    public async Task<IReadOnlyCollection<LinuxBlockDto>> GetAllAsync()
    {
        IReadOnlyCollection<LinuxBlock> linuxBlocks = await _linuxBlockRepository.GetAllAsync();

        return [.. linuxBlocks.Select(linuxBlock => new LinuxBlockDto
        {
            Id = linuxBlock.Id,
            Name = linuxBlock.Name,
            LinuxCommands = linuxBlock.LinuxCommands.Select(linuxCommand => new LinuxCommandDto
            {
                Id = linuxCommand.Id,
                Name = linuxCommand.Name,
                Content = linuxCommand.Content
            }).ToList().AsReadOnly()
        })];
    }

    public async Task AddAsync(LinuxBlockDto linuxBlockDto)
    {
        if (linuxBlockDto.Name.Length > 200)
        {
            throw new Exception("The Name must be no longer than 200 characters.");
        }

        if (await _linuxBlockRepository.ExistsByNameAsync(linuxBlockDto.Name))
        {
            throw new Exception("A block with the name already exists.");
        }

        LinuxBlock linuxBlock = new()
        {
            Name = linuxBlockDto.Name
        };

        await _linuxBlockRepository.AddAsync(linuxBlock);
    }

    public async Task EditAsync(LinuxBlockDto linuxBlockDto)
    {
        LinuxBlock linuxBlock = await _linuxBlockRepository.GetByIdAsync(linuxBlockDto.Id) ?? throw new Exception("Linux Block not found.");

        if (linuxBlockDto.Name.Length > 200)
        {
            throw new Exception("The Name must be no longer than 200 characters.");
        }

        linuxBlock.Name = linuxBlockDto.Name;

        await _linuxBlockRepository.EditAsync(linuxBlock);
    }

    public async Task DeleteAsync(Guid id)
    {
        LinuxBlock linuxBlock = await _linuxBlockRepository.GetByIdAsync(id) ?? throw new Exception("Linux Block not found.");

        if (linuxBlock.LinuxCommands.Count > 0)
        {
            throw new Exception("Block has child entries.");
        }

        await _linuxBlockRepository.DeleteAsync(id);
    }
}