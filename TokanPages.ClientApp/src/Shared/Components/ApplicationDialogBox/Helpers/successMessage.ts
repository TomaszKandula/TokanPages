import { IRaiseDialog } from "../../../../Store/States/raiseDialogState";
import { IconType } from "../../../../Shared/enums";

const SuccessMessage = (title: string, text: string): IRaiseDialog =>
{
    return {
        title: title,
        message: text,
        icon: IconType.info
    };
}

export default SuccessMessage;
