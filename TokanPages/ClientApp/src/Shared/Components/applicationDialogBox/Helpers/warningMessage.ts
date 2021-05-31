import { IRaiseDialog } from "../../../../Redux/States/raiseDialogState";
import { IconType } from "../../../../Shared/enums";

export default function WarningMessage(title: string, text: string): IRaiseDialog
{
    return {
        title: title + " | Warning",
        message: text,
        icon: IconType.warning
    };
}
