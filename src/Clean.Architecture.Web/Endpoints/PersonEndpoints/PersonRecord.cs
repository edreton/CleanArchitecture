namespace Clean.Architecture.Web.Endpoints.PersonEndpoints;

public record PersonRecord(int Id, string FirstName, string LastName, string Email, string PhoneNumber, int Age);
