import { Item } from "../../../Shared/Components/ListRender/Models";

export interface NavigationContentDto {
    content: {
        language: string;
        logo: string;
        menu: {
            image: string;
            items: Item[];
        };
    };
}
