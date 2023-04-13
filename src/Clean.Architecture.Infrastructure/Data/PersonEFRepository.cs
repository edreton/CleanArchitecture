using Ardalis.Specification.EntityFrameworkCore;
using Clean.Architecture.SharedKernel.Interfaces;

namespace Clean.Architecture.Infrastructure.Data;
public class PersonEFRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
  public PersonEFRepository(PersonDbContext dbContext) : base(dbContext)
  {
  }

}
