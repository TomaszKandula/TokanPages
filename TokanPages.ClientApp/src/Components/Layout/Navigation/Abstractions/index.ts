import { NavigationContentDto } from "../../../../Api/Models";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { UseDimensionsResult } from "../../../../Shared/Hooks/useDimensions";
import { ApplicationLanguageState } from "../../../../Store/States";

export interface BaseProperties extends ViewProperties {
    isAnonymous: boolean;
    isMenuOpen: boolean;
    isBottomSheetOpen: boolean;
    media: UseDimensionsResult;
    triggerSideMenu: () => void;
    triggerBottomSheet: () => void;
    infoHandler: () => void;
    aliasName: string;
    avatarName: string;
    avatarSource: string;
    navigation: NavigationContentDto;
    backNavigationOnly?: boolean;
    backPathFragment?: string;
    backPathHandler: () => void;
    languages: ApplicationLanguageState;
    languageId: string;
    languagePickHandler: (id: string) => void;
    languageMenuHandler: () => void;
    isLanguageMenuOpen: boolean;
}

export interface NavigationViewProps extends BaseProperties {
    height?: number;
}

export interface LanguageSelectionProps {
    selection: string;
    languageId: string;
    size?: number;
}

export interface ListSeparatorProps {
    length: number;
    index: number;
}
