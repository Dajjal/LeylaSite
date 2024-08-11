namespace Core.Domain.Generics;

/// <summary>
///     Абстрактная универсальная модель, содержащая идентификатор и флаг логического удаления.
/// </summary>
/// <typeparam name="TKey">Тип данных для идентификатора первичного ключа.</typeparam>
public abstract class GenericEntity<TKey> : IGenericEntity<TKey>
{
    /// <summary>
    ///     Флаг, указывающий, что объект логически удалён.
    ///     Логическое удаление предполагает, что объект не удаляется физически из базы данных,
    ///     а помечается как удалённый для исключения из выборок.
    /// </summary>
    public virtual bool IsDeleted { get; set; }

    /// <summary>
    ///     Идентификатор сущности — первичный ключ. Может быть переопределён в производных классах.
    /// </summary>
    public virtual TKey? Id { get; set; }
}