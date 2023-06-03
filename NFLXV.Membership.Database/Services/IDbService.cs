using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NFLXV.Membership.Database.Services;

public interface IDbService
{
    Task<List<TDto>> GetAsync<TEntity, TDto>()
    where TEntity : class, IEntity
    where TDto : class;

    Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
        where TEntity : class, IEntity
        where TDto : class;

    Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
        where TEntity : class, IEntity;

    Task<bool> SaveChangesAsync();

    Task<TEntity> AddAsync<TEntity, TDto>(TDto dto)
        where TEntity : class, IEntity
        where TDto : class;

    Task<TReferenceEntity> AddAsyncRefEntity<TReferenceEntity, TDto>(TDto dto)
    where TReferenceEntity : class, IReferenceEntity
    where TDto : class;

    void Update<TEntity, TDto>(int id, TDto dto)
        where TEntity : class,
        IEntity where TDto : class;

    public void Include<TEntity>()
    where TEntity : class, IEntity;

    public string GetURL<TEntity>(TEntity entity) where TEntity : class, IEntity;

    public string GetURLRef<TReferenceEntity, TDto>(TDto dto) where TReferenceEntity : class, IReferenceEntity;



    Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IEntity;

    bool DeleteAsyncRefEntity<TReferenceEntity, TDto>(TDto dto) where TReferenceEntity : class where TDto : class;
    void IncludeRef<TReferenceEntity>() where TReferenceEntity : class, IReferenceEntity;
}
