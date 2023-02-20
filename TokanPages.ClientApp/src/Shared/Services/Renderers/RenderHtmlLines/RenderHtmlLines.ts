import Validate from "validate.js";
import { RenderHtmlLine } from "../RenderHtmlLine/renderHtmlLine";
import { RenderHtmlLinesInput } from "./interface";

export const RenderHtmlLines = (props: RenderHtmlLinesInput): string =>
{
    let result: string = "";
    let htmlLine: string = "";

    for (let item of props.inputArray)
    {
        htmlLine = RenderHtmlLine({ tag: props.tag, text: item }); 
        if (!Validate.isEmpty(htmlLine)) result = result.concat(htmlLine);
    }

    return result;
}
