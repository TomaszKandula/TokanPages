import { ItemDto } from "./Common/itemDto";

export interface NavigationContentDto {
    language: string;
    logo: string;
    languageMenu: LanguageMenuProps;
    signup: OptionsProps;
    signout: OptionsProps;
    userInfo: UserInfoProps;
    menu: MenuProps;
}

export interface MenuProps {
    image: string;
    items: ItemDto[];
}

export interface LanguageMenuProps {
    caption: string;
}

export interface OptionsProps {
    caption: string;
    link: string;
    icon: string;
}

export interface UserInfoProps {
    textUserAlias: string;
    textRegistered: string;
    textRoles: string;
    textPermissions: string;
    textButton: string;
}
