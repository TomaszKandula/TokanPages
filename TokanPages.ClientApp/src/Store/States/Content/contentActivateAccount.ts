import { IActivateAccountContentDto } from "../../../Api/Models";

export interface IContentActivateAccount extends IActivateAccountContentDto
{
    isLoading: boolean;
}