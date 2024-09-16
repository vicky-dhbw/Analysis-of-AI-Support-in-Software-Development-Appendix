private _moveSubItems(
    subItems: Array<ConfiguratorSubitem>,
    subItemsSelected: Array<string>,
    direction: MoveSubItemDirection,
): Array<ConfiguratorSubitem> {
    // Group sub-items
    const subItemGroups: Array<Array<ConfiguratorSubitem>> = [];
    let currentGroup: Array<ConfiguratorSubitem> = [];

    subItems.forEach((subItem) => {
        if (!isSubItemDependent(subItem)) {
            if (currentGroup.length > 0) {
                subItemGroups.push(currentGroup);
            }
            currentGroup = [subItem];
        } else {
            currentGroup.push(subItem);
        }
    });

    if (currentGroup.length > 0) {
        subItemGroups.push(currentGroup);
    }

    // Find indices of selected groups
    const selectedIndices = subItemGroups.reduce((acc, group, index) => {
        if (group.some((item) => subItemsSelected.includes(item.id))) {
            acc.push(index);
        }
        return acc;
    }, [] as number[]);

    // Split groups into before, selection, and after
    const beforeIndex = selectedIndices[0];
    const afterIndex = selectedIndices[selectedIndices.length - 1];

    const before = subItemGroups.slice(0, beforeIndex);
    const selection = subItemGroups.slice(beforeIndex, afterIndex + 1);
    const after = subItemGroups.slice(afterIndex + 1);

    // Move items
    if (direction === MoveSubItemDirection.DOWN && after.length > 0) {
        // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
        before.push(after.shift()!);
    } else if (direction === MoveSubItemDirection.UP && before.length > 0) {
        // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
        after.unshift(before.pop()!);
    }

    // Combine and flatten the result
    return [...before, ...selection, ...after].reduce((acc, group) => acc.concat(group), []);
}