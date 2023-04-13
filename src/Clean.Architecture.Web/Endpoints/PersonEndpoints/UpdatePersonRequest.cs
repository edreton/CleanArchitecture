using System.ComponentModel.DataAnnotations;

namespace Clean.Architecture.Web.Endpoints.PersonEndpoints;

public class UpdatePersonRequest
{
  public const string Route = "/Persons";
  [Required]
  public int Id { get; set; }
  [Required]
  public string? FirstName { get; set; }
  [Required]
  public string? LastName { get; set; }
  [Required]
  public string? Email { get; set; }
  [Required]
  public string? PhoneNumber { get; set; }
  [Required]
  public int Age { get; set; }
}
