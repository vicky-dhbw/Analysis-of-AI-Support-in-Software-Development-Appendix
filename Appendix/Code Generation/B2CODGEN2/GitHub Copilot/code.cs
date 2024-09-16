public async Task<ResultBase<List<LinkDto>>> Handle(CreateLinksCommand request, CancellationToken cancellationToken)
{
   // Retrieve the list of existing links from the repository
   var existingLinksResult = await _mediator.Send(new GetLinkListCommand(), cancellationToken);
   var existingLinks = existingLinksResult.Result;

   // Filter out the links that already exist in the repository
   var newLinks = request.Links
                         .Where(linkDto => !existingLinks!.Any(existingLink => existingLink.Url == linkDto.Url))
                         .ToList();

   // Add new links to the repository
   var addedLinks = new List<LinkDto>();
   foreach (var linkDto in newLinks)
   {
       var link = new Link(linkDto.Url, linkDto.Title, linkDto.Description);
       var addedLink = await _repository.AddAsync(link, cancellationToken);
       addedLinks.Add(new LinkDto(addedLink.Id, addedLink.Url, addedLink.Title, addedLink.Description));
   }

   // Save changes to the repository
   await _repository.SaveAsync(cancellationToken);

   // Return the newly added links
   return addedLinks;
}