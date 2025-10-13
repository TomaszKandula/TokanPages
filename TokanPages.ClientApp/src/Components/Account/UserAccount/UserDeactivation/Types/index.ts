import { SectionAccountDeactivation } from "../../../../../Api/Models";
import { ViewProperties } from "../../../../../Shared/types";

export interface UserDeactivationProps {
    className?: string;
}

export interface UserDeactivationViewProps extends ViewProperties, UserDeactivationProps {
    isMobile: boolean;
    buttonHandler: () => void;
    progress: boolean;
    section: SectionAccountDeactivation;
}
