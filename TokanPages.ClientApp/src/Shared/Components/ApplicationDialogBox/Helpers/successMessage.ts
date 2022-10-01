import { IApplicationDialog } from "../../../../Store/States";
import { IconType } from "../../../../Shared/enums";

const SuccessMessage = (title: string, text: string): IApplicationDialog =>
{
    return {
        title: title,
        message: text,
        icon: IconType.info
    };
}

export default SuccessMessage;
