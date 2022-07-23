import { IItem } from "../../../Shared/Components/ListRender/Models";

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
