import { GetTextErrorInput } from "./interface";

export const GetTextError = (props: GetTextErrorInput): string =>
{
    return props.template.replace("{ERROR}", props.error);
}
