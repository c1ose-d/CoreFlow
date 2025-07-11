namespace CoreFlow.Application.Services;

public class DveIdService(IDveIdRepository dveIdRepository, IDveBlockRepository dveBlockRepository) : IDveIdService
{
    private readonly IDveIdRepository _dveIdRepository = dveIdRepository;
    private readonly IDveBlockRepository _dveBlockRepository = dveBlockRepository;

    public async Task<DveIdDto?> GetByIdAsync(Guid id)
    {
        DveId? dveId = await _dveIdRepository.GetByIdAsync(id);

        return dveId == null ? throw new Exception("Dve Id not found.") : new DveIdDto
        {
            Id = dveId.Id,
            Name = dveId.Name,
            Content = dveId.Content,
            BlockId = dveId.BlockId,
            DveBlockDto = dveId.Block == null ? null : new DveBlockDto
            {
                Id = dveId.Block.Id,
                Name = dveId.Block.Name
            }
        };
    }

    public async Task<IReadOnlyCollection<DveIdDto>> GetAllAsync()
    {
        IReadOnlyCollection<DveId> dveIds = await _dveIdRepository.GetAllAsync();

        return [.. dveIds.Select(dveId => new DveIdDto
        {
            Id = dveId.Id,
            Name = dveId.Name,
            Content = dveId.Content,
            BlockId = dveId.BlockId,
            DveBlockDto = dveId.Block == null ? null : new DveBlockDto
            {
                Id = dveId.Block.Id,
                Name = dveId.Block.Name
            }
        })];
    }

    public async Task AddAsync(DveIdDto dveIdDto)
    {
        DveBlock? dveBlock = await _dveBlockRepository.GetByIdAsync(dveIdDto.BlockId) ?? throw new Exception("Dve Block not found.");

        if (dveIdDto.Name.Length > 200)
        {
            throw new Exception("The Name must be no longer than 200 characters.");
        }

        DveId dveId = new()
        {
            Name = dveIdDto.Name,
            Content = dveIdDto.Content,
            Block = dveBlock
        };

        await _dveIdRepository.AddAsync(dveId);
    }

    public async Task EditAsync(DveIdDto dveIdDto)
    {
        DveId? dveId = await _dveIdRepository.GetByIdAsync(dveIdDto.Id) ?? throw new Exception("Dve Id not found.");

        if (dveIdDto.Name.Length > 200)
        {
            throw new Exception("The Name must be no longer than 200 characters.");
        }

        DveBlock? dveBlock = await _dveBlockRepository.GetByIdAsync(dveIdDto.BlockId) ?? throw new Exception("Dve Block not found.");

        dveId.Name = dveIdDto.Name;
        dveId.Content = dveIdDto.Content;
        dveId.Block = dveBlock;

        await _dveIdRepository.EditAsync(dveId);
    }

    public async Task DeleteAsync(Guid id)
    {
        _ = await _dveIdRepository.GetByIdAsync(id) ?? throw new Exception("Dve Id not found.");

        await _dveIdRepository.DeleteAsync(id);
    }
}