import { IUserDataDto } from "./userDataDto";

export interface IAuthenticateUserResultDto extends IUserDataDto
{
    userToken: string;
}
