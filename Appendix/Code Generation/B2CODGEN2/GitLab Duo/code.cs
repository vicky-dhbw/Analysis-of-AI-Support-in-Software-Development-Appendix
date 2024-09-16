public async Task<ResultBase<List<LinkDto>>> Handle(CreateLinksCommand command, CancellationToken cancellationToken)
{
   // Retrieve existing links from the database
   var existingLinksResult = await _mediator.Send(new GetLinkListCommand(), cancellationToken);
   var existingLinks = existingLinksResult.Result;

   // Filter out links that already exist in the database
   var newLinks = command.Links.Where(newLink =>
       !existingLinks!.Any(existingLink => existingLink.Url == newLink.Url)).ToList();

   // Create and save new Link objects
   var createdLinks = new List<LinkDto>();
   foreach (var newLink in newLinks)
   {
       var link = new Link
       {
           Url = newLink.Url,
           Title = newLink.Title,
           Description = newLink.Description,
       };

       await _repository.AddAsync(link, cancellationToken);

       // Add the newly created link to the result list
       createdLinks.Add(new LinkDto
       {
           Id = link.Id, // Assuming the database generates and returns the Id
           Url = link.Url,
           Title = link.Title,
           Description = link.Description,
       });
   }

   // Save changes to the repository
   await _repository.SaveAsync(cancellationToken);

   // Return the list of newly created links
   return createdLinks;
}