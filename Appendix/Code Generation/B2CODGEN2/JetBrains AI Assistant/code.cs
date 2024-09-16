public async Task<ResultBase<List<LinkDto>>> Handle(CreateLinksCommand request, CancellationToken cancellationToken)
{
   // Retrieve existing links from the repository using mediator
   var existingLinksResult = await _mediator.Send(new GetLinkListCommand(), cancellationToken);
   var existingLinks = existingLinksResult.Result;

   var newLinks = new List<LinkDto>();

   foreach (var linkDto in request.Links)
   {
       // Check if the link already exists
       if (!existingLinks!.Any(existingLink => existingLink.Url == linkDto.Url))
       {
           // Create a new Link object
           var newLink = new Link
           {
               Url = linkDto.Url,
               Title = linkDto.Title,
               Description = linkDto.Description,
           };

           // Save the new Link to the repository
           await _repository.AddAsync(newLink, cancellationToken);

           newLinks.Add(linkDto);
       }
   }

   // Save changes to the repository
   await _repository.SaveAsync(cancellationToken);

   return newLinks;
}