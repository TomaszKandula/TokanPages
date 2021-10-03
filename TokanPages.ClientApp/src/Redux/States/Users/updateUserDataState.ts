import { IAuthenticateUserResultDto } from "../../../Api/Models";

export interface IUpdateUserData
{
    isShown: boolean;
    userData: IAuthenticateUserResultDto;
}
