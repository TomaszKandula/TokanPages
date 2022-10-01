import { IResetPasswordContentDto } from "../../../Api/Models";

export interface IContentResetPassword extends IResetPasswordContentDto
{
    isLoading: boolean;
}