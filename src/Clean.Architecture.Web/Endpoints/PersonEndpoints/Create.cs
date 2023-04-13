using Clean.Architecture.Core.PersonAggregate;
using Clean.Architecture.SharedKernel.Interfaces;
using FastEndpoints;

namespace Clean.Architecture.Web.Endpoints.PersonEndpoints;

public class Create : Endpoint<CreatePersonRequest, CreatePersonResponse>
{
  private readonly IRepository<Person> _repository;

  public Create(IRepository<Person> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Post(CreatePersonRequest.Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("PersonEndpoints"));
  }

  public override async Task HandleAsync(
    CreatePersonRequest request,
    CancellationToken cancellationToken)
  {
    if (request.FirstName == null)
    {
      ThrowError("FirstName is required");
    }

    if (request.LastName == null)
    {
      ThrowError("LastName is required");
    }

    if (request.Email == null)
    {
      ThrowError("Email is required");
    }
    
    if (request.PhoneNumber == null)
    {
      ThrowError("PhoneNumber is required");
    }

    var newPerson = new Person(request.FirstName, request.LastName, request.Email, request.PhoneNumber, request.Age);
    var createdItem = await _repository.AddAsync(newPerson, cancellationToken);
    var response = new CreatePersonResponse
    (
      id: createdItem.Id,
      firstname: createdItem.FirstName,
      lastname: createdItem.LastName,
      email: createdItem.Email,
      phonenumber: createdItem.PhoneNumber,
      age: createdItem.Age
    );

    await SendAsync(response, cancellation: cancellationToken);
  }

}
