import { ApplicationDialogState } from "../../../../Store/States";
import { IconType } from "../../../../Shared/enums";

export const SuccessMessage = (title: string, text: string): ApplicationDialogState =>
{
    return { title: title, message: text, icon: IconType.info };
}
