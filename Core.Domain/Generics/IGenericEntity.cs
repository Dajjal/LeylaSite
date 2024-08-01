using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Generics;

/// <summary>
///     Интерфейс для универсальной модели
/// </summary>
/// <typeparam name="TKey">Передаётся желаемый тип идентификатора</typeparam>
public interface IGenericEntity<TKey>
{
    /// <summary>
    ///     Идентификатор - Первичный ключ
    /// </summary>
    [Key]
    public TKey? Id { get; set; }
}