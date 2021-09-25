import { IActivateAccountContentDto } from "../../../Api/Models";

export interface IGetActivateAccountContent extends IActivateAccountContentDto
{
    isLoading: boolean;
}