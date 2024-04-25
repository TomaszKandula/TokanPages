import { ContentDto } from "./Common/contentDto";

export interface NewsletterRemoveContentDto {
    content: {
        language: string;
        contentPre: ContentDto;
        contentPost: ContentDto;
    };
}
