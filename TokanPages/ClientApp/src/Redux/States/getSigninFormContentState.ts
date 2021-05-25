import { ISigninFormContentDto } from "../../Api/Models";

export interface IGetSigninFormContent extends ISigninFormContentDto
{
    isLoading: boolean;
}