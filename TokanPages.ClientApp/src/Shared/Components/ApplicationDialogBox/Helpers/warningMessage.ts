import { IApplicationDialog } from "../../../../Store/States";
import { IconType } from "../../../../Shared/enums";

const WarningMessage = (title: string, text: string): IApplicationDialog =>
{
    return {
        title: title + " | Warning",
        message: text,
        icon: IconType.warning
    };
}

export default WarningMessage;
