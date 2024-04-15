export const HaveSpecialCharacter = (value: string): boolean => {
    const characters = ["!", "@", "#", "$", "%", "^", "&", "*"];
    for (const item of value) {
        if (characters.includes(item)) {
            return true;
        }
    }

    return false;
};
