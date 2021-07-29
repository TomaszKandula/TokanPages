import { IItem } from "../../../Shared/Components/ListRender/Models/item";

export interface INavigationContentDto
{
    content: 
    {
        logo: string;
        menu:
        {
            image: string,
            items: IItem[]
        }
    };
}
