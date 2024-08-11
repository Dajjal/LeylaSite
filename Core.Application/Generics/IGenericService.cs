using Ardalis.Specification;
using Core.Domain.Abstracts;

namespace Core.Application.Generics;

/// <summary>
///     Интерфейс универсального сервиса для управления сущностями.
/// </summary>
/// <typeparam name="TEntity">Тип сущности, представляющий таблицу в базе данных.</typeparam>
public interface IGenericService<TEntity>
{
    /// <summary>
    ///     Асинхронно получает список всех элементов, преобразованных в модель для передачи клиенту.
    /// </summary>
    /// <typeparam name="TDto">Тип модели для передачи клиенту.</typeparam>
    /// <param name="cancellationToken">Токен для отмены запроса.</param>
    /// <returns>Список всех записей в таблице, преобразованных в модель для передачи клиенту.</returns>
    Task<List<TDto>> ListAsync<TDto>(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Асинхронно получает список элементов по спецификации, преобразованных в модель для передачи клиенту.
    /// </summary>
    /// <typeparam name="TDto">Тип модели для передачи клиенту.</typeparam>
    /// <param name="specification">Спецификация для фильтрации элементов.</param>
    /// <param name="cancellationToken">Токен для отмены запроса.</param>
    /// <returns>Список записей, соответствующих спецификации, преобразованных в модель для передачи клиенту.</returns>
    Task<List<TDto>> ListAsync<TDto>(
        ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Асинхронно получает запись по идентификатору, преобразованную в модель для передачи клиенту.
    /// </summary>
    /// <typeparam name="TDto">Тип модели для передачи клиенту.</typeparam>
    /// <param name="id">Идентификатор записи.</param>
    /// <param name="cancellationToken">Токен для отмены запроса.</param>
    /// <returns>Запись, преобразованную в модель для передачи клиенту.</returns>
    /// <exception cref="KeyNotFoundException">Если запись с указанным идентификатором не найдена.</exception>
    Task<TDto> GetByIdAsync<TDto>(
        Guid id, 
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Асинхронно добавляет запись и возвращает её в модели для передачи клиенту.
    /// </summary>
    /// <typeparam name="TDto">Тип модели для передачи клиенту.</typeparam>
    /// <param name="dto">Модель для добавления.</param>
    /// <param name="cancellationToken">Токен для отмены запроса.</param>
    /// <returns>Добавленная запись, преобразованная в модель для передачи клиенту.</returns>
    /// <exception cref="ArgumentNullException">Если <paramref name="dto" /> является <c>null</c>.</exception>
    Task<TDto> AddAsync<TDto>(
        TDto dto,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Асинхронно обновляет запись по идентификатору и возвращает обновлённую запись в модели для передачи клиенту.
    /// </summary>
    /// <typeparam name="TDto">Тип модели для передачи клиенту.</typeparam>
    /// <param name="id">Идентификатор записи, которую нужно обновить.</param>
    /// <param name="dto">Модель для обновления.</param>
    /// <param name="cancellationToken">Токен для отмены запроса.</param>
    /// <returns>Обновлённая запись, преобразованная в модель для передачи клиенту.</returns>
    /// <exception cref="KeyNotFoundException">Если запись с указанным идентификатором не найдена.</exception>
    /// <exception cref="ArgumentNullException">Если <paramref name="dto" /> является <c>null</c>.</exception>
    /// <exception cref="InvalidOperationException">Если идентификаторы не совпадают или имеют неверный формат.</exception>
    Task<TDto> UpdateAsync<TDto>(
        Guid id, 
        TDto dto, 
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Асинхронно удаляет запись по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор записи для удаления.</param>
    /// <param name="cancellationToken">Токен для отмены запроса.</param>
    /// <returns>Идентификатор удалённой записи.</returns>
    /// <exception cref="KeyNotFoundException">Если запись с указанным идентификатором не найдена.</exception>
    Task<Guid> DeleteAsync(
        Guid id, 
        CancellationToken cancellationToken = default);
}