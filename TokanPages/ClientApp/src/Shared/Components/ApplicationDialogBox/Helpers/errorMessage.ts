import { IRaiseDialog } from "../../../../Redux/States/raiseDialogState";
import { IconType } from "../../../../Shared/enums";

export default function ErrorMessage(title: string, text: string): IRaiseDialog
{
    return {
        title: title + " | Error",
        message: text,
        icon: IconType.error
    };
}
