import { ICountWords } from "./interface";

export const CountWords = (props: ICountWords): number => 
{
    if (props.inputText === undefined) return 0;

    props.inputText = props.inputText.replace(/(^\s*)|(\s*$)/gi,"");
    props.inputText = props.inputText.replace(/[ ]{2,}/gi," ");
    props.inputText = props.inputText.replace(/\n /,"\n");

    return props.inputText.split(" ").length;
}
