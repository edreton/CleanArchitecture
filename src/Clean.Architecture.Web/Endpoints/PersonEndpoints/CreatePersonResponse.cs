namespace Clean.Architecture.Web.Endpoints.PersonEndpoints;

public class CreatePersonResponse
{
  public CreatePersonResponse(int id, string firstname, string lastname, string email, string phonenumber, int age)
  {
    Id = id;
    FirstName = firstname;
    LastName = lastname;
    Email = email;
    PhoneNumber = phonenumber;
    Age = age;
  }

  public int? Id { get; private set; }
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string PhoneNumber { get; set; } = string.Empty;
  public int Age { get; set; }

}
