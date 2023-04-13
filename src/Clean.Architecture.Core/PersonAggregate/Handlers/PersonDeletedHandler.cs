using Clean.Architecture.Core.PersonAggregate.Events;
using Clean.Architecture.SharedKernel.Interfaces;
using Clean.Architecture.Core.PersonAggregate.Specifications;
using MediatR;
using Ardalis.Result;

namespace Clean.Architecture.Core.PersonAggregate.Handlers;
public class PersonDeletedHandler : INotificationHandler<PersonDeletedEvent>
{
  private readonly IRepository<Person> _repository;

  public PersonDeletedHandler(IRepository<Person> repository)
  {
    _repository = repository;
  }

  public async Task Handle(PersonDeletedEvent domainEvent, CancellationToken cancellationToken)
  {
    var personSpec = new PersonByIdSpec(domainEvent.PersonId);
    var person = await _repository.GetByIdAsync(domainEvent.PersonId, cancellationToken);

    if (person != null)
    {
      await _repository.DeleteAsync(person, cancellationToken);
    }
  }

}
