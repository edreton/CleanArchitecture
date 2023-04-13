using FastEndpoints;
using Ardalis.Result;
using Clean.Architecture.Core.Interfaces;

namespace Clean.Architecture.Web.Endpoints.PersonEndpoints;

public class Delete : Endpoint<DeletePersonRequest>
{

  private readonly IDeletePersonService _deletePersonService;

  public Delete(IDeletePersonService service)
  {
    _deletePersonService = service;
  }

  public override void Configure()
  {
    Delete(DeletePersonRequest.Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("PersonEndpoints"));
  }
  public override async Task HandleAsync(
    DeletePersonRequest request,
    CancellationToken cancellationToken)
  {
    var result = await _deletePersonService.DeletePerson(request.PersonId);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    await SendNoContentAsync(cancellationToken);
  }
}

