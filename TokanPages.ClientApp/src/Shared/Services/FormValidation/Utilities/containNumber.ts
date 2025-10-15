export const ContainNumber = (value: string): boolean => {
    for (let index: number = 0; index < value.length; index++) {
        let charCode = value.charCodeAt(index);
        if (charCode >= 48 && charCode <= 57) {
            return true;
        }
    }

    return false;
};
