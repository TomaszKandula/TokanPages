export const ClassListClear = (list: DOMTokenList): void => {
    while (list.length > 0) {
        const item = list.item(0);
        if (item !== null) {
            list.remove(item);
        }
    }
};
