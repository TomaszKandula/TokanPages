import { IUserPermissionDto, IUserRoleDto } from "..";

export interface IUserDataDto
{
    userId: string;
    aliasName: string
    avatarName: string;
    firstName: string;
    lastName: string; 
    shortBio: string;
    registered: string;
    roles: IUserRoleDto[];
    permissions: IUserPermissionDto[];
}
