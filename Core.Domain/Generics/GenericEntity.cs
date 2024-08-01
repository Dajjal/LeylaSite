namespace Core.Domain.Generics;

/// <summary>
/// Универсальная модель, с инициализацией первичного ключа
/// </summary>
/// <typeparam name="TKey">Передаётся желаемый тип идентификатора первичного ключа</typeparam>
public abstract class GenericEntity<TKey> : IGenericEntity<TKey>
{
    /// <summary>
    /// Перезаписываемый идентификатор - Первичный ключ
    /// </summary>
    public virtual TKey? Id { get; set; }

    /// <summary>
    /// Флаг, что обьект логически удалён
    /// </summary>
    public virtual bool IsDeleted { get; set; }
}