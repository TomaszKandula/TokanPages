import { PropsToFields } from "../../../../Shared/Services/Converters";
import { RenderHtmlLines } from "../../../../Shared/Services/Renderers";

interface Properties {
    object: any;
    template: string;
}

export const GetTextWarning = (props: Properties): string => {
    const input = { object: props.object };
    const fields = PropsToFields(input);
    const result = {
        inputArray: fields,
        tag: "li",
    };

    const lines = RenderHtmlLines(result);
    return props.template.replace("{LIST}", lines);
};
