import { IItem } from "../../../Shared/Components/ListRender/Models";

export interface INavigationContentDto
{
    content: 
    {
        language: string;
        logo: string;
        menu:
        {
            image: string,
            items: IItem[]
        }
    };
}
