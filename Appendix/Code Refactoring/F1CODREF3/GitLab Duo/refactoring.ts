private _copySubItemsToRightPosition(
    configuration: ConfiguratorModel | null,
    position: number,
    subItemIdsToCopy: Array<string>,
): void {
    if (!configuration?.subItems?.length) {
        throw new Error('copySubItemsToPosition: subItems was null or empty');
    }

    const subItemsToCopy = configuration.subItems.filter((x) => subItemIdsToCopy.includes(x.id));

    const copiedSubItems = this._configurationCopyService
        .copySubItems(subItemsToCopy)
        .map((subItem) => ({ ...subItem, id: uuid.v4() }));

    const { before, after } = this._splitSubItemsAtPosition(configuration.subItems, position);

    configuration.subItems = [...before, ...copiedSubItems, ...after];
}

private _splitSubItemsAtPosition(
    subItems: Array<ConfiguratorSubitem>,
    position: number,
): { before: Array<ConfiguratorSubitem>; after: Array<ConfiguratorSubitem> } {
    return subItems.reduce(
        (acc: { before: Array<ConfiguratorSubitem>; after: Array<ConfiguratorSubitem> }, subItem, index) => {
            if (index <= position) {
                acc.before.push(subItem);
            } else {
                acc.after.push(subItem);
            }
            return acc;
        },
        { before: [], after: [] },
    );
}