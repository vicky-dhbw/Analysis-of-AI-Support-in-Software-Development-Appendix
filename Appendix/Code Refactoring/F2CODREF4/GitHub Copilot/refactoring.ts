private _moveSubItems(
    subItems: Array<ConfiguratorSubitem>,
    subItemsSelected: Array<string>,
    direction: MoveSubItemDirection,
): Array<ConfiguratorSubitem> {
    const subItemGroups: Array<Array<ConfiguratorSubitem>> = [];
    const indices: Array<number> = [];
    let groupIndex = -1;

    // Group each subItem with its dependent blinds
    subItems.forEach((subItem) => {
        if (isSubItemDependent(subItem)) {
            subItemGroups[groupIndex].push(subItem);
        } else {
            groupIndex++;
            subItemGroups[groupIndex] = [subItem];

            if (subItemsSelected.includes(subItem.id)) {
                indices.push(groupIndex);
            }
        }
    });

    const before = subItemGroups.filter((_x, currentIndex) => indices.every((index) => currentIndex < index));
    const selection = subItemGroups.filter((_x, currentIndex) => indices.includes(currentIndex));
    const after = subItemGroups.filter((_x, currentIndex) => indices.every((index) => currentIndex > index));

    if (direction === MoveSubItemDirection.DOWN) {
        const firstDownElement = after.shift();
        if (firstDownElement) {
            before.push(firstDownElement);
        }
    } else {
        const firstUpElement = before.pop();
        if (firstUpElement) {
            after.unshift(firstUpElement);
        }
    }

    const result: Array<ConfiguratorSubitem> = [];
    this._addSubItemsToResult(result, before);
    this._addSubItemsToResult(result, selection);
    this._addSubItemsToResult(result, after);

    return result;
}
