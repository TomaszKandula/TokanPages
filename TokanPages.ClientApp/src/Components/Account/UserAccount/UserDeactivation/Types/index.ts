import { SectionAccountDeactivation } from "../../../../../Api/Models";
import { ViewProperties } from "../../../../../Shared/Types";

export interface UserDeactivationProps {
    className?: string;
}

export interface UserDeactivationViewProps extends ViewProperties, UserDeactivationProps {
    isMobile: boolean | null;
    buttonHandler: () => void;
    progress: boolean;
    section: SectionAccountDeactivation;
}
