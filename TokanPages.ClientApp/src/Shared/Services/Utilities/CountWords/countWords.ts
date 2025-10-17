import { CountWordsProps } from "./Types";

export const CountWords = (props: CountWordsProps): number => {
    if (props.inputText === undefined) {
        return 0;
    }

    const filtering = (value: string): boolean => {
        return value != "";
    };

    const result = props.inputText.split(" ").filter(filtering).length;
    return result;
};
