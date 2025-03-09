import { IconType } from "../../../Shared/enums";

export interface ApplicationDialogState {
    title: string | undefined;
    message: string[] | undefined;
    validation?: object | undefined;
    icon: IconType | undefined;
}
