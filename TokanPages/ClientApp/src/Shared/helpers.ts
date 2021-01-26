import Validate from "validate.js";

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

const FormatDateTime = (value: string): string =>
{
    const options = 
    { 
        day: "2-digit", 
        month: "2-digit", 
        year: "numeric", 
        hour: "2-digit", 
        minute: "2-digit" 
    };
    const datetime = value ? new Date(value) : null;

    let result = "n/a";
    if (datetime) result = datetime.toLocaleDateString("en-US", options);

    return result;
}

export 
{
    ConvertPropsToFields,
    HtmlRenderLine,
    HtmlRenderLines,
    FormatDateTime
}
