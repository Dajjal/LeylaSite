using Core.Domain.Generics;

namespace Core.Domain.Abstracts;

/// <summary>
///     Абстрактная модель с базовыми полями (ID, IsDeleted)
/// </summary>
/// <remarks>
///     <para>
///         ID - Идентификатор с типом поля - Guid
///     </para>
///     <para>
///         IsDeleted - Поле по которому ведётся логическое удаление с типом - Boolean
///     </para>
/// </remarks>
public abstract class AbstractGuidModel : GenericEntity<Guid>;