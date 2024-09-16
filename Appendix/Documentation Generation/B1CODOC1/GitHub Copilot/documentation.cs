/// <summary>
/// Handles the CreateLinkCommand and returns the ID of the created link.
/// </summary>
/// <param name="request">The CreateLinkCommand containing the details of the link to be created.</param>
/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
/// <returns>A ResultBase object containing the ID of the created link if successful.</returns>
public async Task<ResultBase<string>> Handle(CreateLinkCommand request, CancellationToken cancellationToken)
{
    var result = await repository.AddAsync(new Link { Url = request.Dto.Url }, cancellationToken);
    
    await repository.SaveAsync(cancellationToken);

    return result.Id;
}