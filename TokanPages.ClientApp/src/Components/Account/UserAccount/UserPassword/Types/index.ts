import { SectionAccountPassword } from "../../../../../Api/Models";
import { ViewProperties } from "../../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../../Shared/types";

export interface UserPasswordProps {
    className?: string;
}

export interface UserPasswordViewProps extends ViewProperties, UserPasswordProps {
    isMobile: boolean;
    oldPassword: string;
    newPassword: string;
    confirmPassword: string;
    formProgress: boolean;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    buttonHandler: () => void;
    sectionAccountPassword: SectionAccountPassword;
}
