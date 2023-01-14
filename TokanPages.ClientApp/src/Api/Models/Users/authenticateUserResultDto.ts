import { IUserDataDto } from "./userDataDto";
import { IUserRoleDto, IUserPermissionDto } from "..";

export interface IAuthenticateUserResultDto extends IUserDataDto
{
    tokenExpires: string;
    refreshTokenExpires: string;
    userToken: string;
    refreshToken: string;
    roles: IUserRoleDto[];
    permissions: IUserPermissionDto[];
}
