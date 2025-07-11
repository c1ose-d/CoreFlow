namespace CoreFlow.Application.Services;

public class DveBlockService(IDveBlockRepository dveBlockRepository) : IDveBlockService
{
    private readonly IDveBlockRepository _dveBlockRepository = dveBlockRepository;

    public async Task<DveBlockDto?> GetByIdAsync(Guid id)
    {
        DveBlock? dveBlock = await _dveBlockRepository.GetByIdAsync(id);

        return dveBlock == null ? throw new Exception("Dve Block not found.") : new DveBlockDto
        {
            Id = dveBlock.Id,
            Name = dveBlock.Name,
            DveIds = dveBlock.DveIds.Select(dveId => new DveIdDto
            {
                Id = dveId.Id,
                Name = dveId.Name,
                Content = dveId.Content
            }).ToList().AsReadOnly()
        };
    }

    public async Task<IReadOnlyCollection<DveBlockDto>> GetAllAsync()
    {
        IReadOnlyCollection<DveBlock> dveBlocks = await _dveBlockRepository.GetAllAsync();

        return [.. dveBlocks.Select(dveBlock => new DveBlockDto
        {
            Id = dveBlock.Id,
            Name = dveBlock.Name,
            DveIds = dveBlock.DveIds.Select(dveId => new DveIdDto
            {
                Id = dveId.Id,
                Name = dveId.Name,
                Content = dveId.Content
            }).ToList().AsReadOnly()
        })];
    }

    public async Task AddAsync(DveBlockDto dveBlockDto)
    {
        if (dveBlockDto.Name.Length > 200)
        {
            throw new Exception("The Name must be no longer than 200 characters.");
        }

        DveBlock dveBlock = new()
        {
            Name = dveBlockDto.Name
        };

        await _dveBlockRepository.AddAsync(dveBlock);
    }

    public async Task EditAsync(DveBlockDto dveBlockDto)
    {
        DveBlock dveBlock = await _dveBlockRepository.GetByIdAsync(dveBlockDto.Id) ?? throw new Exception("Dve Block not found.");

        if (dveBlockDto.Name.Length > 200)
        {
            throw new Exception("The Name must be no longer than 200 characters.");
        }

        dveBlock.Name = dveBlockDto.Name;

        await _dveBlockRepository.EditAsync(dveBlock);
    }

    public async Task DeleteAsync(Guid id)
    {
        DveBlock dveBlock = await _dveBlockRepository.GetByIdAsync(id) ?? throw new Exception("Dve Block not found.");

        if (dveBlock.DveIds.Count > 0)
        {
            throw new Exception("Block has child entries.");
        }

        await _dveBlockRepository.DeleteAsync(id);
    }
}