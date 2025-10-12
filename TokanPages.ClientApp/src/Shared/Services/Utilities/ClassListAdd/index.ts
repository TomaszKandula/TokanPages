export const ClassListAdd = (list: DOMTokenList, items: string[]): void => {
    if (items.length > 0) {
        items.forEach(item => {
            list.add(item);
        });
    }
}
