using Ardalis.ApiEndpoints;
using Clean.Architecture.Core.PersonAggregate;
using Clean.Architecture.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace Clean.Architecture.Web.Endpoints.PersonEndpoints;

public class Update : EndpointBaseAsync
    .WithRequest<UpdatePersonRequest>
    .WithActionResult<UpdatePersonResponse>
{
  private readonly IRepository<Person> _repository;

  public Update(IRepository<Person> repository)
  {
    _repository = repository;
  }

  [HttpPut(UpdatePersonRequest.Route)]
  [SwaggerOperation(
      Summary = "Updates a Person",
      Description = "Updates a Person.",
      OperationId = "Persons.Update",
      Tags = new[] { "PersonEndpoints" })
  ]
  public override async Task<ActionResult<UpdatePersonResponse>> HandleAsync(
      UpdatePersonRequest request,
      CancellationToken cancellationToken = new())
  {
    if (request.LastName == null)
    {
      return BadRequest();
    }

    var existingPerson = await _repository.GetByIdAsync(request.Id, cancellationToken);
    if (existingPerson == null)
    {
      return NotFound();
    }

    if (request.FirstName != null && request.FirstName != existingPerson.FirstName)
      existingPerson.UpdateName(request.FirstName);

    if (request.LastName != null && request.LastName != existingPerson.LastName)
      existingPerson.UpdateLastName(request.LastName);

    if (request.Email != null && request.Email != existingPerson.Email)
      existingPerson.UpdateEmail(request.Email);

    if (request.PhoneNumber != null && request.PhoneNumber != existingPerson.PhoneNumber)
      existingPerson.UpdatePhoneNumber(request.PhoneNumber);

    if (request.Age > 0 && request.Age != existingPerson.Age)
      existingPerson.UpdateAge(request.Age);

    await _repository.UpdateAsync(existingPerson, cancellationToken);

    var response = new UpdatePersonResponse(
        person: new PersonRecord(existingPerson.Id, existingPerson.FirstName, existingPerson.LastName, existingPerson.Email, existingPerson.PhoneNumber, existingPerson.Age)
    );

    return Ok(response);
  }
}

