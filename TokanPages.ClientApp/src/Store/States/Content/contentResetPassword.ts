import { ResetPasswordContentDto } from "../../../Api/Models";

export interface ContentResetPasswordState extends ResetPasswordContentDto
{
    isLoading: boolean;
}