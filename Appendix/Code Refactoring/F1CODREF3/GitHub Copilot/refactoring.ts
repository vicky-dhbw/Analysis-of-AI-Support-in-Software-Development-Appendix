    private _copySubItemsToRightPosition(
        configuration: ConfiguratorModel | null,
        position: number,
        subItemIdsToCopy: Array<string>,
    ): void {
        if (!configuration?.subItems?.length) {
            throw Error('copySubItemsToPosition: subItems was null or empty');
        }

        const subItemsToCopy = this._getSubItemsToCopy(configuration.subItems, subItemIdsToCopy);
        const copiedSubItems = this._copyAndAssignNewIds(subItemsToCopy);

        const { before, after } = this._splitSubItemsAtPosition(configuration.subItems, position);

        configuration.subItems = [...before, ...copiedSubItems, ...after];
    }

    private _getSubItemsToCopy(
        subItems: Array<ConfiguratorSubitem>,
        subItemIdsToCopy: Array<string>,
    ): Array<ConfiguratorSubitem> {
        return subItems.filter((subItem) => subItemIdsToCopy.includes(subItem.id));
    }

    private _copyAndAssignNewIds(subItems: Array<ConfiguratorSubitem>): Array<ConfiguratorSubitem> {
        const copiedSubItems = this._configurationCopyService.copySubItems(subItems);
        copiedSubItems.forEach((subItem) => (subItem.id = uuid.v4()));
        return copiedSubItems;
    }

    private _splitSubItemsAtPosition(
        subItems: Array<ConfiguratorSubitem>,
        position: number,
    ): { before: Array<ConfiguratorSubitem>; after: Array<ConfiguratorSubitem> } {
        const before: Array<ConfiguratorSubitem> = [];
        const after: Array<ConfiguratorSubitem> = [];

        subItems.forEach((subItem, index) => {
            if (index <= position) {
                before.push(subItem);
            } else {
                after.push(subItem);
            }
        });

        return { before, after };
    }