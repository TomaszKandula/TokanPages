import { IAuthenticateUserResultDto } from "../../../Api/Models";

export interface IStoreUserData
{
    isShown: boolean;
    userData: IAuthenticateUserResultDto;
}
