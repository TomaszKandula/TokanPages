import { IAddUserDto } from "./addUserDto";

export interface IAuthenticateUserResultDto extends IAddUserDto
{
    userToken: string;
}
