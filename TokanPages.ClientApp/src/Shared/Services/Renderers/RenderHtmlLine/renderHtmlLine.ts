import Validate from "validate.js";
import { RenderHtmlLineInput } from "./interface";

export const RenderHtmlLine = (props: RenderHtmlLineInput): string => 
{
    return Validate.isDefined(props.text) ? `<${props.tag}>${props.text}</${props.tag}>` : " ";
}
