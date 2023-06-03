using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NFLXV.Membership.Database.Services;

public class DbService : IDbService
{
    private readonly DbContext _Db;
    private readonly IMapper _mapper;

    public DbService(NFLXVContext db, IMapper mapper)
    {
        _Db = db;
        _mapper = mapper;
    }
    public async Task<List<TDto>> GetAsync<TEntity, TDto>()
    where TEntity : class, IEntity
    where TDto : class
    {
        var entities = await _Db.Set<TEntity>().ToListAsync();
        return _mapper.Map<List<TDto>>(entities);
    }

    public async Task<List<TDto>> GetAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
        where TEntity : class, IEntity
        where TDto : class
    {
        var entities = await _Db.Set<TEntity>().Where(expression).ToListAsync();
        return _mapper.Map<List<TDto>>(entities);
    }

    private async Task<TEntity?> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
        where TEntity : class, IEntity => await _Db.Set<TEntity>().SingleOrDefaultAsync(expression);


    public async Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
        where TEntity : class, IEntity
         where TDto : class
    {
        var entity = await SingleAsync(expression);
        return _mapper.Map<TDto>(entity);
    }

    public void Include<TEntity>()
    where TEntity : class, IEntity
    {
        var propertyNames = _Db.Model.FindEntityType(typeof(TEntity))?.GetNavigations().Select(x => x.Name);

        if (propertyNames is null) return;

        foreach (var name in propertyNames)
            _Db.Set<TEntity>().Include(name).Load();
    }

    public void IncludeRef<TReferenceEntity>()
    where TReferenceEntity : class, IReferenceEntity
    {
        var propertyNames = _Db.Model.FindEntityType(typeof(TReferenceEntity))?.GetNavigations().Select(x => x.Name);

        if (propertyNames is null) return;

        foreach (var name in propertyNames)
            _Db.Set<TReferenceEntity>().Include(name).Load();
    }

    public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
        where TEntity : class, IEntity => await _Db.Set<TEntity>().AnyAsync(expression);


    public async Task<bool> SaveChangesAsync() => await _Db.SaveChangesAsync() >= 0;

    public async Task<TEntity> AddAsync<TEntity, TDto>(TDto dto)
        where TEntity : class, IEntity
        where TDto : class
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _Db.Set<TEntity>().AddAsync(entity);
        return entity;
    }



    public async Task<TReferenceEntity> AddAsyncRefEntity<TReferenceEntity, TDto>(TDto dto)
        where TReferenceEntity : class, IReferenceEntity
        where TDto : class
    {
        var entity = _mapper.Map<TReferenceEntity>(dto);
        await _Db.Set<TReferenceEntity>().AddAsync(entity);
        return entity;
    }

    public void Update<TEntity, TDto>(int id, TDto dto)
         where TEntity : class, IEntity
         where TDto : class
    {
        var entity = _mapper.Map<TEntity>(dto);
        entity.Id = id;
        _Db.Set<TEntity>().Update(entity);
    }

    public async Task<bool> DeleteAsync<TEntity>(int id)
        where TEntity : class, IEntity

    {
        try
        {
            var entity = await SingleAsync<TEntity>(e => e.Id.Equals(id));
            if (entity == null)
            {
                return false;
            }
            _Db.Remove(entity);
        }
        catch
        {
            throw;
        }
        return true;
    }

 
    public bool DeleteAsyncRefEntity<TReferenceEntity, TDto>(TDto dto)
        where TReferenceEntity : class
        where TDto : class
    {
        try
        {
            var entity = _mapper.Map<TReferenceEntity>(dto);
            if (entity == null)
            {
                return false;
            }
            _Db.Remove(entity);
        }
        catch
        {
            throw;
        }
        return true;
    }
    public string GetURL<TEntity>(TEntity entity) where TEntity : class, IEntity
        => $"/{typeof(TEntity).Name.ToLower()}s/{entity.Id}";

    public string GetURLRef<TReferenceEntity, TDto>(TDto dto) where TReferenceEntity : class, IReferenceEntity
        => $"/{typeof(IReferenceEntity).Name.ToLower()}s/{dto}";



}
