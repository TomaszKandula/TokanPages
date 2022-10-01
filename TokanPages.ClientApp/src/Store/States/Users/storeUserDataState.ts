import { IAuthenticateUserResultDto } from "../../../Api/Models";

export interface IUserDataStore
{
    isShown: boolean;
    userData: IAuthenticateUserResultDto;
}
