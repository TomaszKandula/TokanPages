import { IconType } from "../../../Shared/enums";

export interface ApplicationDialogState {
    title: string;
    message: string[] | undefined;
    validation?: object | undefined;
    icon: IconType;
}
