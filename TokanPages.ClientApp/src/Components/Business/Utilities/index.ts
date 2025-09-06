import { OfferItemProps } from "../Types";

export const valueCleanUp = (input: string): string => {
    return input.replaceAll(" ", "").replaceAll("(", "").replaceAll(")", "");
};

export const getSelection = (input?: OfferItemProps[]): string[] => {
    if (!input) {
        return [""];
    }

    const result: string[] = [];
    input.forEach(item => {
        if (item.isChecked) {
            result.push(item.value);
        }
    });

    return result;
};

export const resetSelection = (input: OfferItemProps[]): OfferItemProps[] => {
    const result: OfferItemProps[] = [];
    input.forEach(item => {
        result.push({ ...item, isChecked: false });
    });

    return result;
};
