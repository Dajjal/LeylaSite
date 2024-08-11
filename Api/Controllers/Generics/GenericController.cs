using Core.Application.Generics;
using Core.Application.Specifications;
using Core.Domain.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Generics;

/// <summary>
///     Универсальный контроллер для работы с универсальным сервисом.
/// </summary>
/// <typeparam name="TEntity">Модель описанная в Базе Данных (Таблица).</typeparam>
/// <typeparam name="TDto">Модель для передачи клиенту.</typeparam>
[ApiController]
[Route("api/[controller]")]
public class GenericController<TEntity, TDto>(IGenericService<TEntity> service) : ControllerBase
    where TEntity : AbstractGuidModel
    where TDto : AbstractGuidModel
{
    /// <summary>
    ///     Получает список всех записей.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены запроса.</param>
    /// <returns>Список всех записей в таблице, сконвертированных в модели для передачи клиенту.</returns>
    [HttpGet("All")]
    public async Task<IActionResult> FullListAsync(CancellationToken cancellationToken = default)
    {
        var result = await service.ListAsync<TDto>(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Получает не удалённые записи.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены запроса.</param>
    /// <returns>
    ///     Список записей с использованием спецификации логического удаления.
    ///     Все записи у которых поле IsDeleted = false.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> ListAsync(CancellationToken cancellationToken = default)
    {
        var specification = new NotDeletedSpecification<TEntity>();
        var result = await service.ListAsync<TDto>(specification, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Получает запись по идентификатору - ID.
    /// </summary>
    /// <param name="id">Идентификатор записи.</param>
    /// <param name="cancellationToken">Токен для отмены запроса.</param>
    /// <returns>Запись сконвертированную для передачи клиенту.</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await service.GetByIdAsync<TDto>(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Добавляет запись.
    /// </summary>
    /// <param name="dto">Модель полученная с клиента.</param>
    /// <param name="cancellationToken">Токен для отмены запроса.</param>
    /// <returns>Добавленную запись сконвертированную для передачи клиенту.</returns>
    [HttpPost]
    public async Task<IActionResult> AddAsync(
        [FromBody] TDto dto,
        CancellationToken cancellationToken = default)
    {
        var result = await service.AddAsync(dto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Обновляет запись по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор записи.</param>
    /// <param name="dto">Модель полученная с клиента.</param>
    /// <param name="cancellationToken">Токен для отмены запроса.</param>
    /// <returns>Изменённую запись сконвертированную для передачи клиенту.</returns>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] TDto dto,
        CancellationToken cancellationToken = default)
    {
        var result = await service.UpdateAsync(id, dto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Логически удаляет запись по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор записи для удаления.</param>
    /// <param name="cancellationToken">Токен для отмены запроса.</param>
    /// <returns>Идентификатор удалённой записи.</returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var deletedId = await service.DeleteAsync(id, cancellationToken);
        return Ok(deletedId);
    }
}