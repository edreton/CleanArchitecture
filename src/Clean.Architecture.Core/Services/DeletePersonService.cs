using Ardalis.Result;
using Clean.Architecture.Core.PersonAggregate;
using Clean.Architecture.Core.PersonAggregate.Events;
using Clean.Architecture.Core.Interfaces;
using Clean.Architecture.SharedKernel.Interfaces;
using MediatR;

namespace Clean.Architecture.Core.Services;
public class DeletePersonService : IDeletePersonService
{
  private readonly IRepository<Person> _repository;
  private readonly IMediator _mediator;

  public DeletePersonService(IRepository<Person> repository, IMediator mediator)
  {
    _repository = repository;
    _mediator = mediator;
  }

  public async Task<Result> DeletePerson(int personId)
  {
    var aggregateToDelete = await _repository.GetByIdAsync(personId);
    if (aggregateToDelete == null) return Result.NotFound();

    await _repository.DeleteAsync(aggregateToDelete);
    var domainEvent = new PersonDeletedEvent(personId);
    await _mediator.Publish(domainEvent);
    return Result.Success();
  }
}
