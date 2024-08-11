using System.Data;
using System.Data.SqlTypes;
using Ardalis.Specification;
using AutoMapper;
using Core.Domain.Abstracts;

namespace Core.Application.Generics;

/// <summary>
///     Универсальный сервис для работы с универсальным рапозиторием
/// </summary>
/// <param name="repository">Универсальный репозитори с которым нужно вести работы</param>
/// <param name="mapper">Мапер для связи модели в БД и модели передаваемой клиенту</param>
/// <typeparam name="TEntity">Модель описанная в Базе Данных (Таблица)</typeparam>
public class GenericServiceImpl<TEntity>(
    IRepositoryBase<TEntity> repository,
    IMapperBase mapper) : IGenericService<TEntity> where TEntity : AbstractGuidModel
{
    /// <summary>
    ///     Асинхроный список всех элементов
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены запроса</param>
    /// <typeparam name="TDto">Модель для передачи клиенту</typeparam>
    /// <returns>Список всех записей в таблице, сконвертированный в модель для передачи клинету</returns>
    public async Task<List<TDto>> ListAsync<TDto>(CancellationToken cancellationToken = default)
    {
        try
        {
            // Получаем весь список эслементов с таблицы
            var listEntities = await repository.ListAsync(cancellationToken);
            // Мапим список, для передачи списка в видел моделей для клиента
            var listDto = mapper.Map<List<TDto>>(listEntities);
            // Возвращаем список, сконвертированный в виде моделей для клиента
            return listDto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    ///     Асинхронно возвращает список элементов в таблице используя спецификацию
    /// </summary>
    /// <param name="specification">Спецификация для настроек возвращаемых элементов</param>
    /// <param name="cancellationToken">Токен для отмены запроса</param>
    /// <typeparam name="TDto">Модель для передачи клиенту</typeparam>
    /// <returns>Список записей с использованием спецификации, сконвертированный в модель для передачи клинету</returns>
    public async Task<List<TDto>> ListAsync<TDto>(
        ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Получаем список элементов в таблице используя спецификацию
            var listEntities = await repository.ListAsync(specification, cancellationToken);
            // Мапим список, для передачи списка в видел моделей для клиента
            var listDto = mapper.Map<List<TDto>>(listEntities);
            // Возвращаем список, сконвертированный в виде моделей для клиента
            return listDto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    ///     Асинхронно возвращает одину запись из таблицы по идентификатору - ID, если такой есть.
    ///     Если нет то возвращает ошибку <see cref="System.Data.SqlTypes.SqlNullValueException" />
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен для отмены запроса</param>
    /// <typeparam name="TDto">Модель для передачи клиенту</typeparam>
    /// <returns>Запись в таблице, сконвертированную в модель для передачи клиенту</returns>
    /// <exception cref="System.Data.SqlTypes.SqlNullValueException">
    ///     <see cref="TEntity" /> with ID: {<paramref name="id" />} not found
    /// </exception>
    public async Task<TDto> GetByIdAsync<TDto>(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Находим запись в таблице по идентификатору - ID
            var entity = await repository.GetByIdAsync(id, cancellationToken);
            // Если запись не найдена, возвращаем ошибку
            if (entity is null)
                throw new SqlNullValueException($"{typeof(TEntity).Name.Replace("Model", "")} with ID: {id} not found");
            // Мапим модель таблицы в модель для клиента
            var dto = mapper.Map<TDto>(entity);
            // Возвращаем модель для клиента
            return dto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    ///     Асинхронно добавляет запись в таблицу<br />
    ///     — Параметр {<paramref name="dto" />} не может быть пустым.
    ///     Иначе выдаст ошибку <see cref="ArgumentNullException" /><br />
    ///     — Id - не обязательное поле, автоматически генерурется <see cref="Guid" />,
    ///     но ни-кто не запрещал самому генерировать и отправлять.
    ///     Но будьте осторожны, должна соблюдаться уникальность первичного ключа<br />
    ///     — IsDeleted - не обязательное поле, значение по умолчанию <see cref="Boolean" /> = false
    /// </summary>
    /// <param name="dto">Модель полученная с клиента</param>
    /// <param name="cancellationToken">Токен для отмены запроса</param>
    /// <typeparam name="TDto">Модель для передачи клиенту</typeparam>
    /// <returns>
    ///     Новую добавленную запись с присвоенным идентификатором,
    ///     сконвертированную в модель для передачи клиенту.
    /// </returns>
    /// <exception cref="ArgumentNullException">DataTransferObject is not provided</exception>
    public async Task<TDto> AddAsync<TDto>(
        TDto dto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Проверяем передана ли модель с клиента для добваления, если модель не передана, возвращаем ошибку
            if (dto is null) throw new ArgumentNullException("DataTransferObject is not provided");
            // Мапим модель пришедшую из клиента, в модель таблицы
            var entity = mapper.Map<TEntity>(dto);
            // Добавляем запись в таблицу
            await repository.AddAsync(entity, cancellationToken);
            // Мапим модель таблицы в модель для клиента
            dto = mapper.Map<TDto>(entity);
            // Возвращаем модель для клиента
            return dto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    ///     Асинхронное изменение записи в таблице, если такой есть.<br />
    ///     Если нет то возвращает ошибку <see cref="SqlNullValueException" /><br />
    ///     — Параметр {<paramref name="dto" />} не может быть пустым.
    ///     Иначе выдаст ошибку <see cref="ArgumentNullException" /><br />
    ///     — Тип поля {<paramref name="id" />} передаваемый в параметрах,
    ///     и тип поля {<paramref name="dto" />.id} должны быть одинаковыми!
    ///     Иначе выдаст ошибку <see cref="FormatException" /><br />
    ///     — {<paramref name="id" />} передаваемый в параметрах, и {<paramref name="dto" />.id} должны совпадать!
    ///     Иначе выдаст ошибку <see cref="DataException" />
    /// </summary>
    /// <param name="id">Идентификатор записи которую нужно изменить</param>
    /// <param name="dto">Модель полученная с клиента</param>
    /// <param name="cancellationToken">Токен для отмены запроса</param>
    /// <typeparam name="TDto">Модель для передачи клиенту</typeparam>
    /// <returns>Изменённую запись, сконвертированную в модель для передачи клиенту</returns>
    /// <exception cref="System.Data.SqlTypes.SqlNullValueException">
    ///     <see cref="TEntity" /> with ID: {<paramref name="id" />} not found
    /// </exception>
    /// <exception cref="ArgumentNullException">DataTransferObject is not provided</exception>
    /// <exception cref="FormatException">PrimaryKey types not same</exception>
    /// <exception cref="DataException">Provided ID not same with DataTransferObject ID</exception>
    public async Task<TDto> UpdateAsync<TDto>(
        Guid id,
        TDto dto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Находим запись в таблице по идентификатору - ID
            var entity = await repository.GetByIdAsync(id, cancellationToken);
            // Если запись не найдена, возвращаем ошибку
            if (entity is null)
                throw new SqlNullValueException($"{typeof(TEntity).Name.Replace("Model", "")} with ID: {id} not found");
            // Проверяем передана ли модель с клиента для изменения, если модель не передана, возвращаем ошибку
            if (dto is null) throw new ArgumentNullException("DataTransferObject is not provided");
            // Проверяем на равенство типов идентификаторов - в данном случае Guid у модели переданной с клиента
            // Если типы полей идентификаторов не идентичны, возвращаем ошибку
            if (nameof(Guid) != dto.GetType().GetProperty("Id")?.PropertyType.Name)
                throw new FormatException("PrimaryKey types not same");
            // Проверяем на равенство идентификаторов, переданных с параметров и модели переданной с клиента
            // Если идентификаторы не равны, возвращаем ошибку
            if (!Equals(id, dto.GetType().GetProperty("Id")?.GetValue(dto)))
                throw new DataException("Provided ID not same with DataTransferObject ID");
            // Мапим модель пришедшую из клиента, в модель таблицы
            mapper.Map(dto, entity);
            // Обновляем запись в таблице
            await repository.UpdateAsync(entity, cancellationToken);
            // Мапим модель таблицы в модель для клиента
            mapper.Map(entity, dto);
            // Возвращаем модель для клиента
            return dto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    ///     Асинхронно удаляет запись с таблицы, если такой есть.
    ///     Если нет то возвращает ошибку <see cref="SqlNullValueException" /><br />
    /// </summary>
    /// <param name="id">Идентификатор записи которую нужно удалить</param>
    /// <param name="cancellationToken">Токен для отмены запроса</param>
    /// <returns>Идентификатор удалённой записи</returns>
    /// <exception cref="SqlNullValueException">
    ///     <see cref="TEntity" /> with ID: {<paramref name="id" />} not found
    /// </exception>
    public async Task<Guid> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Находим запись в таблице по идентификатору - ID
            var entity = await repository.GetByIdAsync(id, cancellationToken);
            // Если запись не найдена, возвращаем ошибку
            if (entity is null)
                throw new SqlNullValueException($"{typeof(TEntity).Name.Replace("Model", "")} with ID: {id} not found");
            // Изменяем значение поля - IsDeleted = true
            entity.GetType().GetProperty("IsDeleted")?.SetValue(entity, true);
            // Обновляем запись в таблице
            await repository.UpdateAsync(entity, cancellationToken);
            // Возвращаем идентификатор - ID
            return id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}