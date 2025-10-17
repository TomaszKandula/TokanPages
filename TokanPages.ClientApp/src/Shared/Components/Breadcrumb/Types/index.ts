import { UseDimensionsResult } from "Shared/Hooks";
import { ItemDto, NavigationContentDto, UserInfoProps } from "../../../../Api/Models";

export interface BreadcrumbBaseProps {
    isLoading: boolean;
    downloadUrl?: string;
}

export interface BreadcrumbProps extends BreadcrumbBaseProps {
    watchparam?: string;
}

export interface BreadcrumbViewProps extends BreadcrumbBaseProps {
    media: UseDimensionsResult;
    hasParam: boolean;
    paramValue: string | undefined;
    navigation: NavigationContentDto;
    onBackToRoot: () => void;
    onBackToPrevious: () => void;
}

export interface NavigationProps {
    language: string;
    logo: string;
    userInfo: UserInfoProps;
    menu: {
        image: string;
        items: ItemDto[];
    };
}

export interface PathProps {
    pathname: string;
    navigation: NavigationProps;
}

export interface PathToRootTextResultProps {
    value: string;
    hasHash: boolean;
}

export interface MakeStyledBreadcrumbProps {
    pathname: string;
    navigation: NavigationProps;
    onClick: () => void;
}
