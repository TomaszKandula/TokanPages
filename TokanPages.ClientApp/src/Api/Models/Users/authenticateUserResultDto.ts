import { UserDataDto } from "./userDataDto";
import { UserRoleDto, UserPermissionDto } from "..";

export interface AuthenticateUserResultDto extends UserDataDto
{
    tokenExpires: string;
    refreshTokenExpires: string;
    userToken: string;
    refreshToken: string;
    roles: UserRoleDto[];
    permissions: UserPermissionDto[];
}
