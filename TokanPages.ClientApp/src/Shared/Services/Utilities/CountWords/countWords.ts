import { CountWordsInput } from "./interface";

export const CountWords = (props: CountWordsInput): number => 
{
    if (props.inputText === undefined) 
    {
        return 0;
    }

    const filtering = (value: string): boolean => 
    {
        return value != ""
    }

    const result = props.inputText.split(" ").filter(filtering).length;
    return result;
}
