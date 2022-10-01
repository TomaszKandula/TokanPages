import { INewsletterContentDto } from "../../../Api/Models";

export interface IContentNewsletter extends INewsletterContentDto
{
    isLoading: boolean;
}