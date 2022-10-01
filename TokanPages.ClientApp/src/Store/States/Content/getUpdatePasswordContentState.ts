import { IUpdatePasswordContentDto } from "../../../Api/Models";

export interface IContentUpdatePassword extends IUpdatePasswordContentDto
{
    isLoading: boolean;
}