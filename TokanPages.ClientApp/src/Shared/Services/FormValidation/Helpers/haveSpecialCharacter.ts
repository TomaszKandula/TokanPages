export const HaveSpecialCharacter = (value: string): boolean => {
    const characters = ["!", "@", "#", "$", "%", "^", "&", "*"];
    for (let index: number = 0; index < value.length; index++) {
        if (characters.includes(value[index])) {
            return true;
        }
    }

    return false;
};
