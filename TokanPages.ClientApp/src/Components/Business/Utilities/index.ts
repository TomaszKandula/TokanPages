import { OfferItemDto } from "../../../Api/Models";

export const valueCleanUp = (input: string): string => {
    return input.replaceAll(" ", "").replaceAll("(", "").replaceAll(")", "");
};

export const getSelection = (input?: OfferItemDto[]): string[] => {
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

export const resetSelection = (input?: OfferItemDto[]): OfferItemDto[] => {
    if (!input) {
        return [];
    }

    const result: OfferItemDto[] = [];
    input.forEach(item => {
        result.push({ ...item, isChecked: false });
    });

    return result;
};
