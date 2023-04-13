using Clean.Architecture.SharedKernel;

namespace Clean.Architecture.Core.PersonAggregate.Events;
public class PersonDeletedEvent : DomainEventBase
{
  public int PersonId { get; set; }

  public PersonDeletedEvent(int personId)
  {
    PersonId = personId;
  }
}
