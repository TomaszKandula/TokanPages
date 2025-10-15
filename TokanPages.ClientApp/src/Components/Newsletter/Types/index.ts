import { ContentDto } from "../../../Api/Models";
import { ReactChangeEvent, ReactKeyboardEvent, ViewProperties } from "../../../Shared/Types";

export interface ExtendedViewProps {
    className?: string;
    background?: string;
}

export interface NewsletterViewProps extends ViewProperties {
    caption: string;
    text: string;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    email: string;
    buttonHandler: () => void;
    progress: boolean;
    buttonText: string;
    labelEmail: string;
    className?: string;
}

export interface NewsletterUpdateProps extends ExtendedViewProps {
    id: string;
}

export interface NewsletterUpdateViewProps extends ViewProperties, ExtendedViewProps {
    isMobile: boolean;
    caption: string;
    formHandler: (event: ReactChangeEvent) => void;
    email: string;
    buttonHandler: () => void;
    buttonState: boolean;
    progress: boolean;
    buttonText: string;
    labelEmail: string;
}

export interface NewsletterRemoveViewProps extends ViewProperties, ExtendedViewProps {
    isMobile: boolean;
    hasEmptyId: boolean;
    contentPre: ContentDto;
    contentPost: ContentDto;
    buttonHandler: () => void;
    buttonState: boolean;
    progress: boolean;
    isRemoved: boolean;
}
