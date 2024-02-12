import Validate from "validate.js";

interface Properties {
    tag: string;
    text: string | undefined;
}

export const RenderHtmlLine = (props: Properties): string => {
    return Validate.isDefined(props.text) ? `<${props.tag}>${props.text}</${props.tag}>` : " ";
};
