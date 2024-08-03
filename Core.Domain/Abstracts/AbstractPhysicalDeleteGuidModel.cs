using Core.Domain.Generics;

namespace Core.Domain.Abstracts;

public abstract class AbstractPhysicalDeleteGuidModel : IGenericEntity<Guid>
{
    public Guid Id { get; set; }
}