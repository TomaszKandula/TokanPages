import { IRaiseDialog } from "../../../../Redux/States/raiseDialogState";
import { IconType } from "../../../../Shared/enums";

export default function SuccessMessage(title: string, text: string): IRaiseDialog
{
    return {
        title: title,
        message: text,
        icon: IconType.info
    };
}
