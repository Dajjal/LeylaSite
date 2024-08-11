using Core.Application.Generics;
using Core.Application.Specifications;
using Core.Domain.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Generics;

/// <summary>
///     Универсальный контроллер для работы с универсальным сервисом
/// </summary>
/// <param name="service">Универсальный сервис с которым нужно вести работы</param>
/// <typeparam name="TEntity">Модель описанная в Базе Данных (Таблица)</typeparam>
/// <typeparam name="TDto">Модель для передачи клиенту</typeparam>
[ApiController]
[Route("api/[controller]")]
public class GenericController<TEntity, TDto>(IGenericService<TEntity> service)
    : ControllerBase where TEntity : AbstractGuidModel
{
    /// <summary>
    ///     API - который возвратит список всех записей
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены запроса</param>
    /// <returns>Список всех записей в таблице, сконвертированных в модели для передачи клинету в виде массива</returns>
    [HttpGet("All")]
    public virtual async Task<IActionResult> FullListAsync(CancellationToken cancellationToken = default)
    {
        var result = await service.ListAsync<TDto>(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     API - который возвращает не удалённые записи
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены запроса</param>
    /// <returns>
    ///     Список записей с использованием спецификаций логического удаления. Все записи у которых поле -
    ///     IsDeleted = true. Сконвертированы в модель для передачи клинету в виде массива
    /// </returns>
    [HttpGet]
    public virtual async Task<IActionResult> ListAsync(CancellationToken cancellationToken = default)
    {
        var specification = new NotDeletedSpecification<TEntity>();
        var result = await service.ListAsync<TDto>(specification, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     API - который возвращает запись по иднетификатору - ID
    /// </summary>
    /// <param name="id">Идентификатор <see cref="Guid" /></param>
    /// <param name="cancellationToken">Токен для отмены запроса</param>
    /// <returns>Запись сконвертированную для передачи клиенту</returns>
    [HttpGet("{id:guid}")]
    public virtual async Task<IActionResult> GetByIdAsync([FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await service.GetByIdAsync<TDto>(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     API - который добавляет запись
    /// </summary>
    /// <param name="dto">Модель полученная с клиента</param>
    /// <param name="cancellationToken">Токен для отмены запроса</param>
    /// <returns>Добавленную запись сконвертированную для передачи клиенту</returns>
    [HttpPost]
    public virtual async Task<IActionResult> AddAsync([FromBody] TDto dto,
        CancellationToken cancellationToken = default)
    {
        var result = await service.AddAsync(dto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     API - который изменяет запись
    /// </summary>
    /// <param name="id">Идентификатор <see cref="Guid" /></param>
    /// <param name="dto">Модель полученная с клиента</param>
    /// <param name="cancellationToken">Токен для отмены запроса</param>
    /// <returns>Изменённую запись сконвертированную для передачи клиенту</returns>
    [HttpPut("{id:guid}")]
    public virtual async Task<IActionResult> UpdateAsync([FromRoute] Guid id,
        [FromBody] TDto dto,
        CancellationToken cancellationToken = default)
    {
        var result = await service.UpdateAsync(id, dto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     API - который логически удаляет запись
    /// </summary>
    /// <param name="id">Идентификатор <see cref="Guid" /></param>
    /// <param name="cancellationToken">Токен для отмены запроса</param>
    /// <returns>Идентификатор <see cref="Guid" /> удалённой записи</returns>
    [HttpDelete("{id:guid}")]
    public virtual async Task<IActionResult> DeleteAsync([FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await service.DeleteAsync(id, cancellationToken);
        return Ok(result);
    }
}