export interface SendMessageDto {
    languageId: string;
    firstName: string;
    lastName: string;
    userEmail: string;
    emailFrom: string;
    emailTos: string[];
    subject: string;
    message: string;
    businessData?: string;
}
