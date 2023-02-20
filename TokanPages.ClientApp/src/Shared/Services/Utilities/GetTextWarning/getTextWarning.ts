import { PropsToFieldsInput, PropsToFields } from "../../../../Shared/Services/Converters";
import { RenderHtmlLinesInput, RenderHtmlLines } from "../../../../Shared/Services/Renderers";
import { GetTextWarningInput } from "./interface";

export const GetTextWarning = (props: GetTextWarningInput): string =>
{
    const input: PropsToFieldsInput = 
    {
        object: props.object
    }
    
    const fields = PropsToFields(input);

    const result: RenderHtmlLinesInput = 
    {
        inputArray: fields,
        tag: "li"
    }
    
    const lines = RenderHtmlLines(result);

    return props.template.replace("{LIST}", lines);
}
