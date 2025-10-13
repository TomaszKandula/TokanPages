import { SectionAccountRemoval } from "../../../../../Api/Models";
import { ViewProperties } from "../../../../../Shared/Types";

export interface UserRemovalProps {
    className?: string;
}

export interface UserRemovalViewProps extends ViewProperties, UserRemovalProps {
    isMobile: boolean;
    deleteButtonHandler: () => void;
    deleteAccountProgress: boolean;
    sectionAccountRemoval: SectionAccountRemoval;
}
