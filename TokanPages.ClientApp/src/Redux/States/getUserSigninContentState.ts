import { IUserSigninContentDto } from "../../Api/Models";

export interface IGetUserSigninContent extends IUserSigninContentDto
{
    isLoading: boolean;
}