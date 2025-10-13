import { LinkDto, WarningPasswordDto } from "../../../../Api/Models";
import { ReactChangeEvent, ReactKeyboardEvent, ViewProperties } from "../../../../Shared/types";

export interface UserSignupProps {
    className?: string;
}

export interface UserSignupViewProps extends ViewProperties, UserSignupProps {
    caption: string;
    warning: WarningPasswordDto;
    consent: string;
    button: string;
    link: LinkDto;
    buttonHandler: () => void;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    progress: boolean;
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    terms?: boolean;
    labelFirstName: string;
    labelLastName: string;
    labelEmail: string;
    labelPassword: string;
}

export interface RenderSignupCardProps extends UserSignupViewProps {
    className: string;
}

export interface RenderNotificationProps extends UserSignupViewProps {
    className: string;
}
