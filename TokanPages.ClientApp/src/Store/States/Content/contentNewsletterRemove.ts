import { NewsletterRemoveContentDto } from "../../../Api/Models";

export interface ContentNewsletterRemoveState extends NewsletterRemoveContentDto {
    isLoading: boolean;
}
