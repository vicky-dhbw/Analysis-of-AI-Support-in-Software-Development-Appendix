/// <summary>
/// Retrieves all links with pagination.
/// </summary>
/// <param name="skip">The number of links to skip.</param>
/// <param name="take">The number of links to retrieve.</param>
/// <param name="cancellation">The cancellation token.</param>
/// <returns>A task that represents the asynchronous operation. The task result contains the list of links.</returns>
[HttpGet]
[Route("getAllLinks")]
public async Task<ActionResult<List<LinkDto>>> GetAllLinks([FromQuery] int skip, [FromQuery] int take, CancellationToken cancellation)
{
   var result = await HandleCommandAsync(new GetAllLinksCommand(skip, take), cancellation);
   return Ok(result.Result);
}