using Ardalis.Specification;

namespace Core.Application.Generics;

/// <summary>
///     Интерфейс универсального сервиса
/// </summary>
/// <typeparam name="TEntity">Модель описанная в Базе Данных (Таблица)</typeparam>
public interface IGenericService<TEntity>
{
    /// <summary>
    ///     Асинхроный список всех элементов
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены запроса</param>
    /// <typeparam name="TDto">Модель для передачи клиенту</typeparam>
    /// <returns>Список всех записей в таблице, сконвертированный в модель для передачи клинету</returns>
    Task<List<TDto>> ListAsync<TDto>(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Асинхронно возвращает список элементов в таблице используя спецификацию
    /// </summary>
    /// <param name="specification">Спецификация для настроек возвращаемых элементов</param>
    /// <param name="cancellationToken">Токен для отмены запроса</param>
    /// <typeparam name="TDto">Модель для передачи клиенту</typeparam>
    /// <returns>Список записей с использованием спецификации, сконвертированный в модель для передачи клинету</returns>
    Task<List<TDto>> ListAsync<TDto>(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default);

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
    Task<TDto> GetByIdAsync<TDto>(Guid id, CancellationToken cancellationToken = default);

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
    Task<TDto> AddAsync<TDto>(TDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Асинхронное изменение записи в таблице, если такой есть.<br />
    ///     Если нет то возвращает ошибку <see cref="System.Data.SqlTypes.SqlNullValueException" /><br />
    ///     — Параметр {<paramref name="dto" />} не может быть пустым.
    ///     Иначе выдаст ошибку <see cref="ArgumentNullException" /><br />
    ///     — Тип поля {<paramref name="id" />} передаваемый в параметрах,
    ///     и тип поля {<paramref name="dto" />.id} должны быть одинаковыми!
    ///     Иначе выдаст ошибку <see cref="FormatException" /><br />
    ///     — {<paramref name="id" />} передаваемый в параметрах, и {<paramref name="dto" />.id} должны совпадать!
    ///     Иначе выдаст ошибку <see cref="System.Data.DataException" />
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
    /// <exception cref="System.Data.DataException">Provided ID not same with DataTransferObject ID</exception>
    Task<TDto> UpdateAsync<TDto>(Guid id, TDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Асинхронно удаляет запись с таблицы, если такой есть.
    ///     Если нет то возвращает ошибку <see cref="System.Data.SqlTypes.SqlNullValueException" /><br />
    /// </summary>
    /// <param name="id">Идентификатор записи которую нужно удалить</param>
    /// <param name="cancellationToken">Токен для отмены запроса</param>
    /// <returns>Идентификатор удалённой записи</returns>
    /// <exception cref="System.Data.SqlTypes.SqlNullValueException">
    ///     <see cref="TEntity" /> with ID: {<paramref name="id" />} not found
    /// </exception>
    Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}