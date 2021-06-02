import { IFooterContentIconDto } from "./footerContentIconDto";

export interface IFooterContentDto
{
    content: 
    {
        terms: string,
        policy: string,
        copyright: string,
        reserved: string,
        icons: IFooterContentIconDto[]
    };
}
