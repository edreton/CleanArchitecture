using Ardalis.GuardClauses;
using Clean.Architecture.Core.PersonAggregate.Events;
using Clean.Architecture.SharedKernel;
using Clean.Architecture.SharedKernel.Interfaces;

namespace Clean.Architecture.Core.PersonAggregate;
public class Person : EntityBase, IAggregateRoot
{
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string PhoneNumber { get; set; } = string.Empty;
  public int Age { get; set; }

  public Person(string firstName, string lastName, string email, string phoneNumber, int age)
  {
    FirstName = Guard.Against.NullOrEmpty(firstName, nameof(firstName));
    LastName = Guard.Against.NullOrEmpty(lastName, nameof(lastName));
    Email = Guard.Against.NullOrEmpty(email, nameof(email));
    PhoneNumber = Guard.Against.NullOrEmpty(phoneNumber, nameof(phoneNumber));
    Age = Guard.Against.NegativeOrZero(age, nameof(age));
  }

  public bool IsDone { get; private set; }

  public void MarkComplete()
  {
    if (!IsDone)
    {
      IsDone = true;

      RegisterDomainEvent(new PersonCompletedEvent(this));
    }
  }

  public void AddPerson(int personId)
  {
    Guard.Against.Null(personId, nameof(personId));
    Id = personId;

    var personAddedEvent = new PersonAddedEvent(this, personId);
    RegisterDomainEvent(personAddedEvent);
  }

  public void UpdateName(string newName)
  {
    FirstName = Guard.Against.NullOrEmpty(newName, nameof(newName));
  }

  public void UpdateLastName(string newLastName)
  {
    LastName = Guard.Against.NullOrEmpty(newLastName, nameof(newLastName));
  }

  public void UpdateEmail(string newEmail)
  {
    Email = Guard.Against.NullOrEmpty(newEmail, nameof(newEmail));
  }

  public void UpdatePhoneNumber(string newPhoneNumber)
  {
    PhoneNumber = Guard.Against.NullOrEmpty(newPhoneNumber, nameof(newPhoneNumber));
  }

  public void UpdateAge(int newAge)
  {
    Age = Guard.Against.NegativeOrZero(newAge, nameof(newAge));
  }

  public void UpdateAll(Person person)
  {
    FirstName = Guard.Against.NullOrEmpty(person.FirstName, nameof(person.FirstName));
    LastName = Guard.Against.NullOrEmpty(person.LastName, nameof(person.LastName));
    Email = Guard.Against.NullOrEmpty(person.Email, nameof(person.Email));
    PhoneNumber = Guard.Against.NullOrEmpty(person.PhoneNumber, nameof(person.PhoneNumber));
    Age = Guard.Against.NegativeOrZero(person.Age, nameof(person.Age));
  }

  public override string ToString()
  {
    var status = IsDone ? "Done!" : "Not done.";
    return $"{base.Id}: Status: {status} - {FirstName} {LastName}";
  }
}


