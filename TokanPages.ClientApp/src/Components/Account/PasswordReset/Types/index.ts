import { ReactChangeEvent, ReactKeyboardEvent, ViewProperties } from "../../../../Shared/types";

export interface PasswordResetProps {
    className?: string;
}

export interface Properties extends ViewProperties, PasswordResetProps {
    isMobile: boolean;
    progress: boolean;
    caption: string;
    button: string;
    email: string;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    buttonHandler: () => void;
    labelEmail: string;
}
