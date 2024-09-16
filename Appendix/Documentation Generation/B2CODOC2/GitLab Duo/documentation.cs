/// <summary>
/// Retrieves a paginated list of all links.
/// </summary>
/// <param name="skip">The number of items to skip before starting to collect the result set.</param>
/// <param name="take">The number of items to return in the result set.</param>
/// <param name="cancellation">A cancellation token that can be used to cancel the operation.</param>
/// <returns>An ActionResult containing a List of LinkDto objects representing the links.</returns>
/// <remarks>
/// This endpoint supports pagination through the 'skip' and 'take' parameters.
/// The method uses a GetAllLinksCommand to retrieve the data.
/// </remarks>
[HttpGet]
[Route("getAllLinks")]
public async Task<ActionResult<List<LinkDto>>> GetAllLinks([FromQuery] int skip, [FromQuery] int take, CancellationToken cancellation)
{
    var result = await HandleCommandAsync(new GetAllLinksCommand(skip, take), cancellation);
    return Ok(result.Result);
}