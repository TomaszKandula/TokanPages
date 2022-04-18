import Validate from "validate.js";
import { IRenderHtmlLine } from "./interface";

export const RenderHtmlLine = (props: IRenderHtmlLine): string => 
{
    return Validate.isDefined(Text) ? `<${props.tag}>${props.text}</${props.tag}>` : " ";
}
