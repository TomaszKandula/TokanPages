import { ItemDto } from "./Common/itemDto";

export interface NavigationContentDto {
    language: string;
    logo: string;
    signup: SignupProps;
    userInfo: UserInfoProps;
    menu: {
        image: string;
        items: ItemDto[];
    };
}

export interface SignupProps {
    caption: string;
    link: string;
}

export interface UserInfoProps {
    textUserAlias: string;
    textRegistered: string;
    textRoles: string;
    textPermissions: string;
    textButton: string;
}
