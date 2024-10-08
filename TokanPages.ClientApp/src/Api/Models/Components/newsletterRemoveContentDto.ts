import { ContentDto } from "./Common/contentDto";

export interface NewsletterRemoveContentDto {
    language: string;
    contentPre: ContentDto;
    contentPost: ContentDto;
}
