import Validate from "validate.js";
import { ITextObject } from "./Components/ContentRender/Models/textModel";
import { IErrorDto } from "../Api/Models";
import { UNEXPECTED_ERROR, VALIDATION_ERRORS } from "./constants";
import { RAISE_ERROR } from "../Redux/Actions/raiseErrorAction";

const ConvertPropsToFields = (InputObject: any): any[] =>
{
    let resultArray: any[] = [];

    for (let Property in InputObject) 
        resultArray = resultArray.concat(InputObject[Property]); 
    
    return resultArray;
}

const HtmlRenderLine = (Tag: string, Text: string | undefined): string => 
{
    return Validate.isDefined(Text) ? `<${Tag}>${Text}</${Tag}>` : " ";
}

const HtmlRenderLines = (InputArray: any[], Tag: string): string =>
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
    if (value === "n/a" || value === "" || value === " ") return "n/a";
    
    const formatWithTime: Intl.DateTimeFormatOptions = 
    { 
        day: "2-digit", 
        month: "2-digit", 
        year: "numeric", 
        hour: "2-digit", 
        minute: "2-digit" 
    };

    const formatWithoutTime: Intl.DateTimeFormatOptions = 
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

const GetShortText = (value: string, limit: number): string => 
{
    if (value === undefined || value === "") return "";
    
    let result = value;
    let output: string[] = value.split(/\s+/);

    if (output.length > limit)
    {
        let strings = output.slice(0, limit);
        let lastWord = strings[strings.length - 1];

        strings[strings.length - 1] = lastWord.replace(/[^0-9a-zA-Z ]/, "");
        result = `${strings.join(" ")}...`;
    }

    return result;
}

const GetErrorMessage = (errorObject: any): string =>
{
    console.error(errorObject);
    let result: string = UNEXPECTED_ERROR;

    if (errorObject?.response?.data)
    {
        const parsedJson: IErrorDto = errorObject.response.data as IErrorDto;
        result = Validate.isEmpty(parsedJson.ErrorMessage) 
            ? UNEXPECTED_ERROR 
            : parsedJson.ErrorMessage;

        if (parsedJson.ValidationErrors !== null) 
        {
            result = result + ", " + VALIDATION_ERRORS + ".";
        }
    }

    return result;
}

const RaiseError = (dispatch: any, errorObject: any): string =>
{
    let error = typeof(errorObject) !== "string" 
        ? GetErrorMessage(errorObject) 
        : errorObject;

    dispatch({ type: RAISE_ERROR, errorObject: error });
    return "";
}

const DelDataFromStorage = (key: string): boolean => 
{
    if (Validate.isEmpty(key)) return false;
    localStorage.removeItem(key);
    return true;
}

const SetDataInStorage = (selection: {} | any[], key: string): boolean => 
{
    if (Validate.isEmpty(key)) return false;

    let serialized = JSON.stringify(selection);
    localStorage.setItem(key, serialized);
    return true;
}

const GetDataFromStorage = (key: string): {} | any[] => 
{
    let serialized = localStorage.getItem(key) as string;
    if (Validate.isEmpty(serialized)) return { };

    let deserialized: string | {} | any[] = "";
    try 
    {
        deserialized = JSON.parse(serialized);
    }
    catch
    {
        console.error(`Cannot parse JSON string: ${serialized}.`);
        return { };
    }

    return deserialized;
}

export 
{
    ConvertPropsToFields,
    HtmlRenderLine,
    HtmlRenderLines,
    FormatDateTime,
    TextObjectToRawText,
    CountWords,
    GetReadTime,
    GetShortText,
    GetErrorMessage,
    RaiseError,
    DelDataFromStorage,
    SetDataInStorage,
    GetDataFromStorage
}
