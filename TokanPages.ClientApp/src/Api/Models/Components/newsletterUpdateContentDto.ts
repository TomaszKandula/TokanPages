export interface NewsletterUpdateContentDto {
    content: {
        language: string;
        caption: string;
        button: string;
        labelEmail: string;
    };
}
