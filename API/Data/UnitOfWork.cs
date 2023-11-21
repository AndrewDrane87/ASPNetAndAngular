
using API.Data;
using AutoMapper;
using System.Diagnostics;

namespace API;

public class UnitOfWork 
{
    public readonly DataContext context;
    private readonly IMapper mapper;

    public UnitOfWork(DataContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public IUserRepository UserRepository => new UserRepository(context, mapper);
    public IMessageRepository MessageRepository => new MessageRepository(context, mapper);
    public PlayerCharacterRepository PlayerCharacterRepository => new PlayerCharacterRepository(context, mapper);
    public ItemRepository ItemRepository => new ItemRepository(context, mapper);
    public AdventureRepository AdventureRepository => new AdventureRepository(context, mapper);
    public NpcRepository NpcRepository => new NpcRepository(context, mapper);
    public DialogRepository DialogRepository => new DialogRepository(context, mapper);
    public ContainerRepository ContainerRepository => new ContainerRepository(context, mapper);
    public EnemyRepository EnemyRepository => new EnemyRepository(context);

    public async Task<bool> Complete() 
    {
        var result = await context.SaveChangesAsync();
        Debug.WriteLine("uow.Result = " + result);
        return result > 0;
    }

    public bool HasChanges()
    {
        return context.ChangeTracker.HasChanges();
    }
}
