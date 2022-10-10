import { IApplicationDialog } from "../../../../Store/States";
import { IconType } from "../../../../Shared/enums";

export const ErrorMessage = (title: string, text: string): IApplicationDialog =>
{
    return { title: title, message: text, icon: IconType.error };
}
