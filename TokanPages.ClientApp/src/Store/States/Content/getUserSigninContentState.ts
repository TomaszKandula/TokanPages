import { IUserSigninContentDto } from "../../../Api/Models";

export interface IContentUserSignin extends IUserSigninContentDto
{
    isLoading: boolean;
}