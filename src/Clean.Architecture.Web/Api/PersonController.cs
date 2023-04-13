using Azure.Core;
using Clean.Architecture.Core.PersonAggregate;
using Clean.Architecture.Core.PersonAggregate.Specifications;
using Clean.Architecture.SharedKernel.Interfaces;
using Clean.Architecture.Web.ApiModels;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Architecture.Web.Api;

/// <summary>
/// Old Approach. Controller not in use. Check Endpoints folder.
/// </summary>

public class PersonController : BaseApiController
{
  private readonly IRepository<Person> _repository;
  public PersonController(IRepository<Person> repository)
  {
    _repository = repository;
  }

  // GET: api/Person
  [HttpGet]
  public async Task<IActionResult> List()
  {
    var personDTOs = (await _repository.ListAsync())
        .Select(person => new PersonDTO
        (
            id: person.Id,
            firstname: person.FirstName,
            lastname: person.LastName,
            email: person.Email,
            phonenumber: person.PhoneNumber,
            age: person.Age
        ))
        .ToList();

    return Ok(personDTOs);
  }

  // GET: api/Person
  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetById(int id)
  {
    var personSpec = new PersonByIdSpec(id);
    var person = await _repository.FirstOrDefaultAsync(personSpec);
    if (person == null)
    {
      return NotFound();
    }

    var result = new PersonDTO
    (
        id: person.Id,
        firstname: person.FirstName,
        lastname: person.LastName,
        email: person.Email,
        phonenumber: person.PhoneNumber,
        age: person.Age
    );

    return Ok(result);
  }

  // POST: api/Person
  [HttpPost]
  public async Task<IActionResult> Post([FromBody] CreatePersonDTO personDTO)
  {
    var person = new Person(personDTO.FirstName, personDTO.LastName, personDTO.Email, personDTO.PhoneNumber, personDTO.Age);
    await _repository.AddAsync(person);
    return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
  }

  //DELETE: api/Person/5
  [HttpDelete("{id:int}")]
  public async Task<IActionResult> Delete(int id)
  {
    var personSpec = new PersonByIdSpec(id);
    var person = await _repository.FirstOrDefaultAsync(personSpec);
    if (person == null)
    {
      return NotFound();
    }

    await _repository.DeleteAsync(person);
    return NoContent();
  }

  // PUT: api/Person/5
  [HttpPut("{id:int}")]
  public async Task<IActionResult> Put(int id, [FromBody] UpdatePersonDTO personDTO)
  {
    var personSpec = new PersonByIdSpec(id);
    var person = await _repository.FirstOrDefaultAsync(personSpec);
    if (person == null)
    {
      return NotFound();
    }
    
    if (personDTO.FirstName != null && personDTO.FirstName != person.FirstName)
      person.UpdateName(personDTO.FirstName);
    if (personDTO.LastName != null && personDTO.LastName != person.LastName)
      person.UpdateLastName(personDTO.LastName);
    if (personDTO.Email != null && personDTO.Email != person.Email)
      person.UpdateEmail(personDTO.Email);
    if (personDTO.PhoneNumber != null && personDTO.PhoneNumber != person.PhoneNumber)
      person.UpdatePhoneNumber(personDTO.PhoneNumber);
    if (personDTO.Age > 0 && personDTO.Age != person.Age)
      person.UpdateAge(personDTO.Age);

    await _repository.UpdateAsync(person);
    return NoContent();
  }

}
