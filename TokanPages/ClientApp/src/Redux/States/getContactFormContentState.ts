import { IContactFormContentDto } from "../../Api/Models";

export interface IGetContactFormContent extends IContactFormContentDto
{
    isLoading: boolean;
}