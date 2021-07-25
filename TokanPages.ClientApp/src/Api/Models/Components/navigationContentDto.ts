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

interface IFields
{
    id: string;
    name: string,
    link: string,
    icon: string,
    enabled: boolean
}

interface IItem extends IFields
{
    subitems: ISubitem[]
}

interface ISubitem extends IFields { }
