import { IconType } from "../../../Shared/Enums";

export interface ApplicationDialogState {
    title: string | undefined;
    message: string[] | undefined;
    validation?: object | undefined;
    icon: IconType | undefined;
    buttons?: {
        primaryButton?: {
            label?: string;
            action?: () => void;
        };
        secondaryButton?: {
            label?: string;
            action?: () => void;
        };
    };
}
