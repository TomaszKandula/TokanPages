import { ViewProperties } from "../../../../Shared/types";

export interface UserSignoutProps {
    className?: string;
}

export interface UserSignoutViewProps extends ViewProperties, UserSignoutProps {
    languageId: string;
    caption: string;
    status: string[];
    buttonText: string;
    isAnonymous: boolean;
}
