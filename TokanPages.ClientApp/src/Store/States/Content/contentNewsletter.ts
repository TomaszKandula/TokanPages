import { NewsletterContentDto } from "../../../Api/Models";

export interface ContentNewsletterState extends NewsletterContentDto
{
    isLoading: boolean;
}