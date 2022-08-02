import { IIcon } from "./Common/icon";
import { ILink } from "./Common/link";

export interface IFooterContentDto
{
    content: 
    {
        language: string;
        terms: ILink,
        policy: ILink,
        copyright: string,
        reserved: string,
        icons: IIcon[]
    };
}
