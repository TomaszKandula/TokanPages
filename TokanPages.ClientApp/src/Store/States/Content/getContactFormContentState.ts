import { IContactFormContentDto } from "../../../Api/Models";

export interface IContentContactForm extends IContactFormContentDto
{
    isLoading: boolean;
}