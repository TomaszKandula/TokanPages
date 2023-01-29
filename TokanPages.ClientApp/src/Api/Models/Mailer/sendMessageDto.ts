export interface SendMessageDto
{
    firstName: string;
    lastName: string;
    userEmail: string;
    emailFrom: string;
    emailTos: string[];
    subject: string;
    message: string; 
}
