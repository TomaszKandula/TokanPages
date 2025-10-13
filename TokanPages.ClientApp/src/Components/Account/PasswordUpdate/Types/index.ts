import { ReactChangeEvent, ReactKeyboardEvent, ViewProperties } from "../../../../Shared/types";

export interface PasswordUpdateProps {
    className?: string;
}

export interface Properties extends ViewProperties, PasswordUpdateProps {
    isMobile: boolean;
    progress: boolean;
    caption: string;
    button: string;
    newPassword: string;
    verifyPassword: string;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    buttonHandler: () => void;
    disableForm: boolean;
    labelNewPassword: string;
    labelVerifyPassword: string;
}
