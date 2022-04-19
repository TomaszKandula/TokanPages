import { IGetTextError } from "./interface";

export const GetTextError = (props: IGetTextError): string =>
{
    return props.template.replace("{ERROR}", props.error);
}
