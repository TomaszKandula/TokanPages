import { IUserDataDto } from "./userDataDto";
import { IUserRoleDto, IUserPermissionDto } from "..";

export interface IAuthenticateUserResultDto extends IUserDataDto
{
    userToken: string;
    roles: IUserRoleDto[];
    permissions: IUserPermissionDto[];
}
