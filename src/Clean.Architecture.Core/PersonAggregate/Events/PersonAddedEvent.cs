using Clean.Architecture.Core.PersonAggregate;
using Clean.Architecture.SharedKernel;

namespace Clean.Architecture.Core.PersonAggregate.Events;
public class PersonAddedEvent : DomainEventBase
{
  public int PersonId { get; set; }
  public Person Person { get; set; }

  public PersonAddedEvent(Person person, int personId)
  {
    Person = person;
    PersonId = personId;
  }

}
