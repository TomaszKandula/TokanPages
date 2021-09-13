import { IRaiseDialog } from "../../../../Redux/States/raiseDialogState";
import { IconType } from "../../../../Shared/enums";

const ErrorMessage = (title: string, text: string): IRaiseDialog =>
{
    return {
        title: title + " | Error",
        message: text,
        icon: IconType.error
    };
}

export default ErrorMessage;
