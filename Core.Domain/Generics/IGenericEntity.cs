using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Generics;

/// <summary>
///     Интерфейс для универсальной сущности, содержащей первичный ключ.
/// </summary>
/// <typeparam name="TKey">Тип данных для идентификатора сущности.</typeparam>
public interface IGenericEntity<TKey>
{
    /// <summary>
    ///     Идентификатор сущности — первичный ключ.
    /// </summary>
    [Key]
    TKey? Id { get; set; }
}