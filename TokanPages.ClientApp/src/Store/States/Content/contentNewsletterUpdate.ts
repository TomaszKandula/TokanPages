import { NewsletterUpdateContentDto } from "../../../Api/Models";

export interface ContentNewsletterUpdateState extends NewsletterUpdateContentDto {
    isLoading: boolean;
}
