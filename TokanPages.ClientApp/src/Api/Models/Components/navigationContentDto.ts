import { Item } from "../../../Shared/Components/RenderMenu/Models";

export interface NavigationContentDto {
    language: string;
    logo: string;
    userInfo: UserInfoProps;
    menu: {
        image: string;
        items: Item[];
    };
}

export interface UserInfoProps {
    textUserAlias: string;
    textRegistered: string;
    textRoles: string;
    textPermissions: string;
    textButton: string;
}
