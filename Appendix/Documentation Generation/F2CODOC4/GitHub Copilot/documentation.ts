    /**
     * Creates a new link.
     *
     * @param link - The link to be created.
     * @returns A Promise that resolves to void.
     */
    public async createLink(link: Link): Promise<void> {
        link = await lastValueFrom(this._linksConnector.createLink(link));
        this._store.dispatch(LinksActions.setLinkAction({ link }));
    }