import { IconType } from "../../../Shared/enums";

export interface ApplicationDialogState {
    title: string;
    message: string[] | undefined;
    icon: IconType;
}
