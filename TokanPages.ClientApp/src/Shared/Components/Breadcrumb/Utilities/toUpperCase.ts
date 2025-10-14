export const toUpperCase = (value: string | undefined): string => {
    if (value === undefined) {
        return "";
    }

    return value
        .toLowerCase()
        .split(" ")
        .map(function (word) {
            return word[0].toUpperCase() + word.substring(1);
        })
        .join(" ");
};
