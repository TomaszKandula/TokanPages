import { LinkDto } from "./Common/linkDto";

export interface ArticleFeaturesContentDto
{
    content: 
    {
        language: string;
        title: string;
        description: string;
        text1: string;
        text2: string;
        action: LinkDto;
        image1: string;
        image2: string;
        image3: string;
        image4: string;
    };
}
