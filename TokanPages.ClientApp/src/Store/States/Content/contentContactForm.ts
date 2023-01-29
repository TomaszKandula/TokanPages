import { ContactFormContentDto } from "../../../Api/Models";

export interface ContentContactFormState extends ContactFormContentDto
{
    isLoading: boolean;
}