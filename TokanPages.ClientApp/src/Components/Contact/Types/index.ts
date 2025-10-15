import { PresentationDto } from "../../../Api/Models";
import { ReactChangeEvent, ReactChangeTextEvent, ReactKeyboardEvent, ViewProperties } from "../../../Shared/Types";

export interface ContactFormProps {
    hasCaption?: boolean;
    hasIcon?: boolean;
    hasShadow?: boolean;
    className?: string;
    mode: "section" | "page";
}

export interface ContactFormViewProps extends ViewProperties, ContactFormProps {
    caption: string;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    messageHandler: (event: ReactChangeTextEvent) => void;
    firstName: string;
    lastName: string;
    email: string;
    subject: string;
    message: string;
    buttonHandler: () => void;
    progress: boolean;
    buttonText: string;
    consent: string;
    presentation: PresentationDto;
    labelFirstName: string;
    labelLastName: string;
    labelEmail: string;
    labelSubject: string;
    labelMessage: string;
    minRows?: number;
}
