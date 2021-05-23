import { ISignupFormContentDto } from "../../Api/Models";

export interface IGetSignupFormContent extends ISignupFormContentDto
{
    isLoading: boolean;
}