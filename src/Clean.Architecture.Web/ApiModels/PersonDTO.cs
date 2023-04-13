namespace Clean.Architecture.Web.ApiModels;

public class PersonDTO : CreatePersonDTO
{
  public PersonDTO(int id, string firstname, string lastname, string email, string phonenumber, int age) : base(firstname, lastname, email, phonenumber, age)
  {
    Id = id;
  }

  public int Id { get; set; }
}

public abstract class CreatePersonDTO
{
  protected CreatePersonDTO(string firstname, string lastname, string email, string phonenumber, int age)
  {
    FirstName = firstname;
    LastName = lastname;
    Email = email;
    PhoneNumber = phonenumber;
    Age = age;
  }

  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string PhoneNumber { get; set; } = string.Empty;
  public int Age { get; set; }


}


public abstract class UpdatePersonDTO
{
  protected UpdatePersonDTO(string firstname, string lastname, string email, string phonenumber, int age)
  {
    FirstName = firstname;
    LastName = lastname;
    Email = email;
    PhoneNumber = phonenumber;
    Age = age;
  }

  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string PhoneNumber { get; set; } = string.Empty;
  public int Age { get; set; }


}
