import { ViewProperties } from "../../../../Shared/Abstractions";

export interface ExtendedViewProps {
    className?: string;
}

export interface AccountActivateProps extends ExtendedViewProps {
    id: string;
    type: string;
}

export interface AccountActivateViewProps extends ViewProperties, ExtendedViewProps {
    shouldFallback: boolean;
    caption: string;
    text1: string;
    text2: string;
    fallback: {
        caption: string;
        text: string[];
    };
    hasProgress: boolean;
    hasError: boolean;
    hasSuccess: boolean;
}
