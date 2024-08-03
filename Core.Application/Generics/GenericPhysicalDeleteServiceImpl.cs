using System.Data;
using System.Data.SqlTypes;
using Ardalis.Specification;
using AutoMapper;
using Core.Domain.Abstracts;

namespace Core.Application.Generics;

public class GenericPhysicalDeleteServiceImpl<TEntity>(
    IRepositoryBase<TEntity> repository,
    IMapperBase mapper) : IGenericService<TEntity>
    where TEntity : AbstractPhysicalDeleteGuidModel
{
    public async Task<List<TDto>> ListAsync<TDto>(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var listEntities = await repository.ListAsync(cancellationToken);
            var listDto = mapper.Map<List<TDto>>(listEntities);
            return listDto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<TDto>> ListAsync<TDto>(
        ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var listEntities = await repository.ListAsync(specification, cancellationToken);
            var listDto = mapper.Map<List<TDto>>(listEntities);
            return listDto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<TDto> GetByIdAsync<TDto>(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await repository.GetByIdAsync(id, cancellationToken);
            if (entity is null)
                throw new SqlNullValueException($"{typeof(TEntity).Name.Replace("Model", "")} with ID: {id} not found");
            var dto = mapper.Map<TDto>(entity);
            return dto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<TDto> AddAsync<TDto>(
        TDto dto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));
            var entity = mapper.Map<TEntity>(dto);
            await repository.AddAsync(entity, cancellationToken);
            dto = mapper.Map<TDto>(entity);
            return dto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<TDto> UpdateAsync<TDto>(
        Guid id,
        TDto dto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Checking for lice
            if (dto is null) throw new ArgumentNullException(nameof(dto));
            var entity = await repository.GetByIdAsync(id, cancellationToken);
            if (entity is null)
                throw new SqlNullValueException($"{typeof(TEntity).Name.Replace("Model", "")} with ID: {id} not found");
            if (nameof(Guid) != dto.GetType().GetProperty("Id")?.PropertyType.Name)
                throw new FormatException("PrimaryKey types not same");
            if (!Equals(id, dto.GetType().GetProperty("Id")?.GetValue(dto)))
                throw new DataException("Provided ID not same with DataTransferObject ID");
            // Updating data
            mapper.Map(dto, entity);
            await repository.UpdateAsync(entity, cancellationToken);
            mapper.Map(entity, dto);
            return dto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<Guid> DeleteLogicalAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> DeletePhysicalAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await repository.GetByIdAsync(id, cancellationToken);
            if (entity is null)
                throw new SqlNullValueException($"{typeof(TEntity).Name.Replace("Model", "")} with ID: {id} not found");
            await repository.DeleteAsync(entity, cancellationToken);
            return id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}