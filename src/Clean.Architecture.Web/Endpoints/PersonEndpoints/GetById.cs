using Ardalis.ApiEndpoints;
using Clean.Architecture.Core.PersonAggregate;
using Clean.Architecture.Core.PersonAggregate.Specifications;
using Clean.Architecture.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Clean.Architecture.Web.Endpoints.PersonEndpoints;

public class GetById : EndpointBaseAsync
  .WithRequest<GetPersonByIdRequest>
  .WithActionResult<GetPersonByIdResponse>
{
  private readonly IRepository<Person> _repository;

  public GetById(IRepository<Person> repository)
  {
    _repository = repository;
  }

  [HttpGet(GetPersonByIdRequest.Route)]
  [SwaggerOperation(
    Summary = "Gets a single Person",
    Description = "Gets a single Person by Id",
    OperationId = "Persons.GetById",
    Tags = new[] { "PersonEndpoints" })
  ]
  public override async Task<ActionResult<GetPersonByIdResponse>> HandleAsync(
    [FromRoute] GetPersonByIdRequest request,
    CancellationToken cancellationToken = new())
  {
    var spec = new PersonByIdSpec(request.PersonId);
    var entity = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
    if (entity == null)
    {
      return NotFound();
    }

    var response = new GetPersonByIdResponse
    (
      id: entity.Id,
      firstname: entity.FirstName,
      lastname: entity.LastName,
      email: entity.Email,
      phonenumber: entity.PhoneNumber,
      age: entity.Age
    );

    return Ok(response);
  }

}
