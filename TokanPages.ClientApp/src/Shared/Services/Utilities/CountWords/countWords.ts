import { ICountWords } from "./interface";

export const CountWords = (props: ICountWords): number => 
{
    if (props.inputText === undefined) return 0;
    return props.inputText.split(" ").filter(function(n) { return n != '' }).length;
}
