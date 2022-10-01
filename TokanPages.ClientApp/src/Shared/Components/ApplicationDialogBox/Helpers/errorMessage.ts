import { IApplicationDialog } from "../../../../Store/States";
import { IconType } from "../../../../Shared/enums";

const ErrorMessage = (title: string, text: string): IApplicationDialog =>
{
    return {
        title: title + " | Error",
        message: text,
        icon: IconType.error
    };
}

export default ErrorMessage;
