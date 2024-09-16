/// <summary>
/// Handles the CreateLinkCommand by adding a new link to the repository.
/// </summary>
/// <param name="request">The CreateLinkCommand object containing the link data.</param>
/// <param name="cancellationToken">The cancellation token.</param>
/// <returns>Returns a ResultBase string object.</returns>
public async Task<ResultBase<string>> Handle(CreateLinkCommand request, CancellationToken cancellationToken)
{
   var result = await repository.AddAsync(new Link { Url = request.Dto.Url, }, cancellationToken);
   await repository.SaveAsync(cancellationToken);

   return result.Id;
}