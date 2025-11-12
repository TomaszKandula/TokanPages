import { ReactChangeEvent, ReactKeyboardEvent, ViewProperties } from "../../../../Shared/Types";

export interface PasswordUpdateProps {
    className?: string;
}

export interface Properties extends ViewProperties, PasswordUpdateProps {
    isMobile: boolean | null;
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
