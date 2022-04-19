import { IPropsToFields, PropsToFields } from "../../../../Shared/Services/Converters";
import { IRenderHtmlLines, RenderHtmlLines } from "../../../../Shared/Services/Renderers";
import { IGetTextWarning } from "./interface";

export const GetTextWarning = (props: IGetTextWarning): string =>
{
    const input: IPropsToFields = 
    {
        object: props.object
    }
    
    const fields = PropsToFields(input);

    const result: IRenderHtmlLines = 
    {
        inputArray: fields,
        tag: "li"
    }
    
    const lines = RenderHtmlLines(result);

    return props.template.replace("{LIST}", lines);
}
