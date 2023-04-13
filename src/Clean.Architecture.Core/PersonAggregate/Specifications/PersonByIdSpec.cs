using Ardalis.Specification;
using Clean.Architecture.Core.ContributorAggregate;

namespace Clean.Architecture.Core.PersonAggregate.Specifications;
public class PersonByIdSpec : Specification<Person>, ISingleResultSpecification
{
  public PersonByIdSpec(int personId)
  {
    Query
        .Where(person => person.Id == personId);
  }

}
