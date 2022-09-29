import { IRaiseDialog } from "../../../../Store/States/raiseDialogState";
import { IconType } from "../../../../Shared/enums";

const WarningMessage = (title: string, text: string): IRaiseDialog =>
{
    return {
        title: title + " | Warning",
        message: text,
        icon: IconType.warning
    };
}

export default WarningMessage;
