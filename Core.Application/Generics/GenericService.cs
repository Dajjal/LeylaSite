using Ardalis.Specification;
using AutoMapper;
using Core.Domain.Abstracts;
using Microsoft.Extensions.Logging;

namespace Core.Application.Generics;

/// <summary>
///     Универсальный сервис для работы с универсальным репозиторием.
/// </summary>
/// <typeparam name="TEntity">Модель, описанная в базе данных (таблица).</typeparam>
public class GenericService<TEntity>(
    IRepositoryBase<TEntity> repository,
    IMapper mapper,
    ILogger<GenericService<TEntity>> logger
) : IGenericService<TEntity>
    where TEntity : AbstractGuidModel
{
    private readonly ILogger<GenericService<TEntity>> _logger =
        logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly IMapper _mapper =
        mapper ?? throw new ArgumentNullException(nameof(mapper));

    private readonly IRepositoryBase<TEntity> _repository =
        repository ?? throw new ArgumentNullException(nameof(repository));

    /// <summary>
    ///     Асинхронно получает список всех элементов и преобразует его в модели для передачи клиенту.
    /// </summary>
    /// <typeparam name="TDto">Модель для передачи клиенту.</typeparam>
    /// <param name="cancellationToken">Токен для отмены запроса.</param>
    /// <returns>Список всех записей в таблице, преобразованных в модели для передачи клиенту.</returns>
    public async Task<List<TDto>> ListAsync<TDto>(CancellationToken cancellationToken = default)
    {
        try
        {
            // Получаем все записи из репозитория
            var entities = await _repository.ListAsync(cancellationToken);
            // Преобразуем записи в модели для клиента
            return _mapper.Map<List<TDto>>(entities);
        }
        catch (Exception ex)
        {
            // Логируем ошибку и выбрасываем исключение
            _logger.LogError(ex, "Ошибка при получении списка всех элементов.");
            throw;
        }
    }

    /// <summary>
    ///     Асинхронно получает список элементов, соответствующих спецификации, и преобразует его в модели для передачи
    ///     клиенту.
    /// </summary>
    /// <typeparam name="TDto">Модель для передачи клиенту.</typeparam>
    /// <param name="specification">Спецификация для настройки возвращаемых элементов.</param>
    /// <param name="cancellationToken">Токен для отмены запроса.</param>
    /// <returns>Список записей, соответствующих спецификации, преобразованных в модели для передачи клиенту.</returns>
    public async Task<List<TDto>> ListAsync<TDto>(
        ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            // Получаем записи из репозитория по спецификации
            var entities = await _repository.ListAsync(specification, cancellationToken);
            // Преобразуем записи в модели для клиента
            return _mapper.Map<List<TDto>>(entities);
        }
        catch (Exception ex)
        {
            // Логируем ошибку и выбрасываем исключение
            _logger.LogError(ex, "Ошибка при получении списка элементов по спецификации.");
            throw;
        }
    }

    /// <summary>
    ///     Асинхронно получает запись по идентификатору и преобразует её в модель для передачи клиенту.
    /// </summary>
    /// <typeparam name="TDto">Модель для передачи клиенту.</typeparam>
    /// <param name="id">Идентификатор записи.</param>
    /// <param name="cancellationToken">Токен для отмены запроса.</param>
    /// <returns>Запись, преобразованная в модель для передачи клиенту.</returns>
    /// <exception cref="KeyNotFoundException">Запись с указанным идентификатором не найдена.</exception>
    public async Task<TDto> GetByIdAsync<TDto>(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            // Находим запись в репозитории по идентификатору
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException($"Запись с ID: {id} не найдена.");

            // Преобразуем запись в модель для клиента
            return _mapper.Map<TDto>(entity);
        }
        catch (Exception ex)
        {
            // Логируем ошибку и выбрасываем исключение
            _logger.LogError(ex, $"Ошибка при получении записи с ID: {id}.");
            throw;
        }
    }

    /// <summary>
    ///     Асинхронно добавляет запись в таблицу и возвращает её в модели для передачи клиенту.
    /// </summary>
    /// <typeparam name="TDto">Модель для передачи клиенту.</typeparam>
    /// <param name="dto">Модель, полученная от клиента.</param>
    /// <param name="cancellationToken">Токен для отмены запроса.</param>
    /// <returns>Добавленная запись, преобразованная в модель для передачи клиенту.</returns>
    /// <exception cref="ArgumentNullException">Модель для добавления не предоставлена.</exception>
    public async Task<TDto> AddAsync<TDto>(
        TDto dto,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(dto);
        try
        {
            // Преобразуем модель клиента в сущность
            var entity = _mapper.Map<TEntity>(dto);
            // Добавляем сущность в репозиторий
            await _repository.AddAsync(entity, cancellationToken);
            // Преобразуем добавленную сущность в модель для клиента
            return _mapper.Map<TDto>(entity);
        }
        catch (Exception ex)
        {
            // Логируем ошибку и выбрасываем исключение
            _logger.LogError(ex, "Ошибка при добавлении новой записи.");
            throw;
        }
    }

    /// <summary>
    ///     Асинхронно обновляет запись по идентификатору и возвращает её в модели для передачи клиенту.
    /// </summary>
    /// <typeparam name="TDto">Модель для передачи клиенту.</typeparam>
    /// <param name="id">Идентификатор записи для обновления.</param>
    /// <param name="dto">Модель, полученная от клиента.</param>
    /// <param name="cancellationToken">Токен для отмены запроса.</param>
    /// <returns>Обновлённая запись, преобразованная в модель для передачи клиенту.</returns>
    /// <exception cref="KeyNotFoundException">Запись с указанным идентификатором не найдена.</exception>
    /// <exception cref="ArgumentNullException">Модель для обновления не предоставлена.</exception>
    /// <exception cref="InvalidOperationException">ID в модели не совпадает с переданным ID.</exception>
    public async Task<TDto> UpdateAsync<TDto>(
        Guid id, TDto dto,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(dto);
        try
        {
            // Находим запись в репозитории по идентификатору
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException($"Запись с ID: {id} не найдена.");

            // Проверяем, что ID в DTO совпадает с переданным ID
            var dtoId = dto.GetType().GetProperty("Id")?.GetValue(dto);
            if (!Equals(id, dtoId))
                throw new InvalidOperationException("ID в DTO не совпадает с переданным ID.");

            // Обновляем сущность и сохраняем изменения
            _mapper.Map(dto, entity);
            await _repository.UpdateAsync(entity, cancellationToken);
            // Преобразуем обновлённую сущность в модель для клиента
            return _mapper.Map<TDto>(entity);
        }
        catch (Exception ex)
        {
            // Логируем ошибку и выбрасываем исключение
            _logger.LogError(ex, $"Ошибка при обновлении записи с ID: {id}.");
            throw;
        }
    }

    /// <summary>
    ///     Асинхронно удаляет запись по идентификатору, помечая её как удалённую.
    /// </summary>
    /// <param name="id">Идентификатор записи для удаления.</param>
    /// <param name="cancellationToken">Токен для отмены запроса.</param>
    /// <returns>Идентификатор удалённой записи.</returns>
    /// <exception cref="KeyNotFoundException">Запись с указанным идентификатором не найдена.</exception>
    public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            // Находим запись в репозитории по идентификатору
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException($"Запись с ID: {id} не найдена.");

            // Помечаем запись как удалённую и сохраняем изменения
            entity.IsDeleted = true;
            await _repository.UpdateAsync(entity, cancellationToken);
            return id;
        }
        catch (Exception ex)
        {
            // Логируем ошибку и выбрасываем исключение
            _logger.LogError(ex, $"Ошибка при удалении записи с ID: {id}.");
            throw;
        }
    }
}