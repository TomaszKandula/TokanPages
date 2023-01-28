import { ICountWords } from "./interface";

export const CountWords = (props: ICountWords): number => 
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
