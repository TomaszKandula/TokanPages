import { IResetPasswordContentDto } from "../../../Api/Models";

export interface IGetResetPasswordContent extends IResetPasswordContentDto
{
    isLoading: boolean;
}