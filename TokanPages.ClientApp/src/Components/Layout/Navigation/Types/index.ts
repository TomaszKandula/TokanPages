import { ItemDto, NavigationContentDto } from "../../../../Api/Models";
import { ViewProperties } from "../../../../Shared/Types";
import { UseDimensionsResult } from "../../../../Shared/Hooks/useDimensions";
import { ApplicationLanguageState } from "../../../../Store/States";

export interface NavigationProps {
    backNavigationOnly?: boolean;
}

export interface NavigationViewBaseProps extends ViewProperties {
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
    backPathHandler: () => void;
    languages: ApplicationLanguageState;
    languageId: string;
    languageFlagDir: string;
    languageFlagType: string;
    languagePickHandler: (id: string) => void;
    languageMenuHandler: () => void;
    isLanguageMenuOpen: boolean;
}

export interface NavigationViewProps extends NavigationViewBaseProps {
    height?: number;
}

export interface RenderLanguageListProps extends NavigationViewProps {
    hasBulmaCells?: boolean;
}

export interface LanguageSelectionProps {
    selection: string;
    languageId: string;
    size?: number;
    className?: string;
}

export interface RenderNavbarMenuBaseProps {
    isAnonymous: boolean;
    languageId: string;
}

export interface RenderNavbarMenuProps extends RenderNavbarMenuBaseProps {
    items: ItemDto[] | undefined;
}

export interface CanContinueProps extends RenderNavbarMenuBaseProps {
    item: ItemDto;
}

export interface RenderSideMenuBaseProps {
    isAnonymous: boolean;
    languageId: string;
}

export interface RenderSideMenuProps extends RenderSideMenuBaseProps {
    items: ItemDto[] | undefined;
}

export interface CanContinueProps extends RenderSideMenuBaseProps {
    item: ItemDto;
}
