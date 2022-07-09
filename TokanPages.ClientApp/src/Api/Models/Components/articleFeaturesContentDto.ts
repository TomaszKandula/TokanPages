import { ILink } from "./Common/link";

export interface IArticleFeaturesContentDto
{
    content: 
    {
        title: string;
        description: string;
        text1: string;
        text2: string;
        action: ILink;
        image1: string;
        image2: string;
        image3: string;
        image4: string;
    };
}
