import { MenuDto } from "./menuDto";

export interface FieldsDto {
    id: string;
    type: string;
    value: string;
    link?: string;
    icon?: string;
    enabled?: boolean;
    sideMenu?: MenuDto;
    navbarMenu?: MenuDto;
}
