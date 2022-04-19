import Validate from "validate.js";
import { RenderHtmlLine } from "../RenderHtmlLine/renderHtmlLine";
import { IRenderHtmlLines } from "./interface";

export const RenderHtmlLines = (props: IRenderHtmlLines): string =>
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
