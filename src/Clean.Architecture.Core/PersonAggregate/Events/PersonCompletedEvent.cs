using Clean.Architecture.Core.PersonAggregate;
using Clean.Architecture.SharedKernel;

namespace Clean.Architecture.Core.PersonAggregate.Events;
public class PersonCompletedEvent : DomainEventBase
{
  public Person CompletedPerson { get; set; }

  public PersonCompletedEvent(Person completedPerson)
  {
    CompletedPerson = completedPerson;
  }

}
