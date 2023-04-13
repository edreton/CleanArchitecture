using Ardalis.ApiEndpoints;
using Clean.Architecture.Core.PersonAggregate;
using Clean.Architecture.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Clean.Architecture.Web.Endpoints.PersonEndpoints;

public class List : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<PersonListResponse>
{
  private readonly IReadRepository<Person> _repository;

  public List(IReadRepository<Person> repository)
  {
    _repository = repository;
  }

  [HttpGet("/Persons")]
  [SwaggerOperation(
      Summary = "Gets a list of all Persons",
      Description = "Gets a list of all Persons",
      OperationId = "Person.List",
      Tags = new[] { "PersonEndpoints" })
  ]
  public override async Task<ActionResult<PersonListResponse>> HandleAsync(
    CancellationToken cancellationToken = new())
  {
    var persons = await _repository.ListAsync(cancellationToken);
    var response = new PersonListResponse
    {
      Persons = persons
          .Select(person => new PersonRecord(person.Id, person.FirstName, person.LastName, person.Email, person.PhoneNumber, person.Age))
          .ToList()
    };

    return Ok(response);
  }
}
