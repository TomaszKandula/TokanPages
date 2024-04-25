export interface UpdateNewsletterContentDto {
    content: {
        language: string;
        caption: string;
        button: string;
        labelEmail: string;
    };
}
