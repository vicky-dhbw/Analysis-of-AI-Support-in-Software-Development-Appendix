/// <summary>
/// Handles the creation of a new link in the system.
/// </summary>
/// <param name="request">The command containing the information for creating a new link.</param>
/// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
/// <returns>
/// A task that represents the asynchronous operation. 
/// The task result contains a ResultBase<string> with the ID of the newly created link.
/// </returns>
public async Task<ResultBase<string>> Handle(CreateLinkCommand request, CancellationToken cancellationToken)
{
    var result = await repository.AddAsync(new Link() { Url = request.Dto.Url, }, cancellationToken);
    await repository.SaveAsync(cancellationToken);
    return result.Id;
}