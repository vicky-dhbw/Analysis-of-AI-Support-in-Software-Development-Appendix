/**
 * Creates a new link and updates the store.
 * 
 * @param {Link} link - The link object to be created.
 * @returns {Promise<void>} A promise that resolves when the link is created and the store is updated.
 * 
 * @description
 * This method performs the following steps:
 * 1. Calls the `createLink` method of the `_linksConnector` to create the link.
 * 2. Dispatches a `setLinkAction` to update the store with the newly created link.
 * 
 * @example
 * const newLink: Link = { ... };
 * await createLink(newLink);
 */
public async createLink(link: Link): Promise<void> {
    link = await lastValueFrom(this._linksConnector.createLink(link));
    this._store.dispatch(LinksActions.setLinkAction({ link }));
}