import Validate from "validate.js";
import { ITextObject } from "./Components/ContentRender/Models/textModel";

const ConvertPropsToFields = (InputObject: any) =>
{
    let resultArray: any[] = [];

    for (let Property in InputObject) 
        resultArray = resultArray.concat(InputObject[Property]); 
    
    return resultArray;
}

const HtmlRenderLine = (Tag: string, Text: string | undefined) => 
{
    return Validate.isDefined(Text) ? `<${Tag}>${Text}</${Tag}>` : " ";
}

const HtmlRenderLines = (InputArray: any[], Tag: string) =>
{
    let result: string = "";
    let htmlLine: string = "";

    for (let Item of InputArray)
    {
        htmlLine = HtmlRenderLine(Tag, Item); 
        if (!Validate.isEmpty(htmlLine)) 
            result = result.concat(htmlLine);
    }

    return result;
}

const FormatDateTime = (value: string, hasTimeVisible: boolean): string =>
{
    const formatWithTime = 
    { 
        day: "2-digit", 
        month: "2-digit", 
        year: "numeric", 
        hour: "2-digit", 
        minute: "2-digit" 
    };

    const formatWithoutTime = 
    {
        day: "2-digit", 
        month: "2-digit", 
        year: "numeric"
    };

    const options = hasTimeVisible ? formatWithTime : formatWithoutTime;    
    const datetime = value ? new Date(value) : null;

    let result = "n/a";
    if (datetime) result = datetime.toLocaleDateString("en-US", options);

    return result;
}

const TextObjectToRawText = (textObject: ITextObject | undefined): string | undefined => 
{
    if (textObject === undefined)
    {
        return undefined;
    }

    if (textObject.items.length === 0)
    {
        return undefined;
    }

    let rawText: string = "";
    let htmlText: string = "";
    let aggregatedText: string = "";
    
    textObject.items.forEach(item => 
    {
        if (item.type === "html")
        {
            htmlText = item.value as string;
            rawText = htmlText.replace(/<[^>]+>/g, " ");
            aggregatedText = aggregatedText + " " + rawText;
        }
    });

    return aggregatedText.trimStart().trimEnd();
}

const CountWords = (inputText: string | undefined): number => 
{
    if (inputText === undefined) return 0;

    inputText = inputText.replace(/(^\s*)|(\s*$)/gi,"");
    inputText = inputText.replace(/[ ]{2,}/gi," ");
    inputText = inputText.replace(/\n /,"\n");

    return inputText.split(" ").length;
}

const GetReadTime = (countWords: number, wordsPerMinute: number): string =>
{
    if (countWords === 0) return "0";
    let result: number = 0;
    result = countWords / wordsPerMinute;
    return result.toFixed(2);
}

export 
{
    ConvertPropsToFields,
    HtmlRenderLine,
    HtmlRenderLines,
    FormatDateTime,
    TextObjectToRawText,
    CountWords,
    GetReadTime
}
