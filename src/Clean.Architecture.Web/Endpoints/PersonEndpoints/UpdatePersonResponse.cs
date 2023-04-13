using Clean.Architecture.Web.ApiModels;

namespace Clean.Architecture.Web.Endpoints.PersonEndpoints;

public class UpdatePersonResponse
{
  public UpdatePersonResponse(PersonRecord person)
  {
    Person = person;
  }

  public PersonRecord Person { get; set; }
}
