using Core.Domain.Generics;

namespace Core.Domain.Abstracts;

/// <summary>
///     Абстрактная модель, содержащая базовые поля, такие как ID и IsDeleted.
///     Этот класс предназначен для сущностей с идентификатором типа <see cref="Guid" />.
/// </summary>
/// <remarks>
///     <para>
///         <see cref="Id" /> - Идентификатор сущности, тип <see cref="Guid" />.
///     </para>
///     <para>
///         <see cref="IsDeleted" /> - Логическое поле, указывающее, удалена ли сущность, тип <see cref="bool" />.
///     </para>
///     <para>
///         Этот класс следует использовать в качестве базового для всех моделей, где требуется стандартный идентификатор
///         и механизм логического удаления.
///     </para>
/// </remarks>
public abstract class AbstractGuidModel : GenericEntity<Guid>
{
    // Дополнительные методы или свойства могут быть добавлены здесь в будущем
}