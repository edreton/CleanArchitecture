using Clean.Architecture.Core.ContributorAggregate;
using Clean.Architecture.Core.PersonAggregate;
using Clean.Architecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Clean.Architecture.Web;

public class SeedPersonData
{
  public static void Initialize(IServiceProvider serviceProvider)
  {
    using (var dbContext = new PersonDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<PersonDbContext>>(), null))
    {
      // Look for any items.
      if (dbContext.Persons.Any())
      {
        return;   // DB has been seeded
      }

      PopulateTestData(dbContext);
    }
  }

  public static void PopulateTestData(PersonDbContext dbContext)
  {
    foreach (var item in dbContext.Persons)
    {
      dbContext.Remove(item);
    }
    dbContext.SaveChanges();

    dbContext.Persons.Add(new Person("John", "Doe", "john_doe@gmail.com", "01555442233", 42));
    dbContext.Persons.Add(new Person("Jane", "Doe", "jane_doe@gmail.com", "05999445566", 38));
    dbContext.Persons.Add(new Person("Joseph", "Smith", "joseph.smith@outlook.com", "08444663322", 35));
    dbContext.SaveChanges();
  }


}
