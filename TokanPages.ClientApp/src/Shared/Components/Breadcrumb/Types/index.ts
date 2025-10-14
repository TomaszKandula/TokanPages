import { UseDimensionsResult } from "Shared/Hooks";
import { ItemDto, NavigationContentDto, UserInfoProps } from "../../../../Api/Models";

export interface CustomBreadcrumbBaseProps {
    isLoading: boolean;
}

export interface CustomBreadcrumbProps extends CustomBreadcrumbBaseProps {
    watchparam?: string;
}

export interface CustomBreadcrumbViewProps extends CustomBreadcrumbBaseProps  {
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
