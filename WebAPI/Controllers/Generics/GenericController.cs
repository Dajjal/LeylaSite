using Core.Application.Generics;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Generics;

[ApiController]
[Route("api/[controller]")]
public class GenericController<TEntity, TDto>(IGenericService<TEntity> service) : BaseController
{
    [HttpGet]
    public virtual async Task<IActionResult> ListAsync(CancellationToken cancellationToken = default)
    {
        var result = await service.ListAsync<TDto>(cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public virtual async Task<IActionResult> GetByIdAsync([FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await service.GetByIdAsync<TDto>(id, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public virtual async Task<IActionResult> AddAsync([FromBody] TDto dto,
        CancellationToken cancellationToken = default)
    {
        var result = await service.AddAsync(dto, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public virtual async Task<IActionResult> UpdateAsync([FromRoute] Guid id,
        [FromBody] TDto dto,
        CancellationToken cancellationToken = default)
    {
        var result = await service.UpdateAsync(id, dto, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public virtual async Task<IActionResult> DeleteAsync([FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await service.DeleteAsync(id, cancellationToken);
        return Ok(result);
    }
}