import { INewsletterContentDto } from "../../Api/Models";

export interface IGetNewsletterContent extends INewsletterContentDto
{
    isLoading: boolean;
}