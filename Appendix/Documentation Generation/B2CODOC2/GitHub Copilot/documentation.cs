/// <summary>
/// Retrieves a paginated list of links.
/// </summary>
/// <param name="skip">The number of links to skip.</param>
/// <param name="take">The number of links to take.</param>
/// <param name="cancellation">The cancellation token.</param>
/// <returns>A list of <see cref="LinkDto"/> objects.</returns>
[HttpGet]
[Route("getAllLinks")]
public async Task<ActionResult<List<LinkDto>>> GetAllLinks([FromQuery] int skip, [FromQuery] int take, CancellationToken cancellation)
{
    var result = await HandleCommandAsync(new GetAllLinksCommand(skip, take), cancellation);
    return Ok(result.Result);
}