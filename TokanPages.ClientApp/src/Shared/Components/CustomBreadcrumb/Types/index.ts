import { ItemDto, UserInfoProps } from "../../../../Api/Models";

export interface CustomBreadcrumbProps {
    isLoading: boolean;
    watchparam?: string;
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
