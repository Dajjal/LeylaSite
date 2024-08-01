using Ardalis.Specification;

namespace Core.Application.Generics;

/// <summary>
///     Интерфейс универсального сервиса
/// </summary>
/// <typeparam name="TEntity">Модель описанная в Базе Данных (Таблица)</typeparam>
public interface IGenericService<TEntity>
{
    Task<List<TDto>> ListAsync<TDto>(CancellationToken cancellationToken = default);

    Task<List<TDto>> ListAsync<TDto>(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default);

    Task<TDto> GetByIdAsync<TDto>(Guid id, CancellationToken cancellationToken = default);

    Task<TDto> AddAsync<TDto>(TDto dto, CancellationToken cancellationToken = default);
    Task<TDto> UpdateAsync<TDto>(Guid id, TDto dto, CancellationToken cancellationToken = default);
    Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}