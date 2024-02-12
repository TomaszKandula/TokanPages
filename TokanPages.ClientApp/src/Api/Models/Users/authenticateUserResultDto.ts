import { UserDataDto } from "./userDataDto";
import { UserRoleDto, UserPermissionDto } from "..";

export interface AuthenticateUserResultDto extends UserDataDto {
    userToken: string;
    refreshToken: string;
    roles: UserRoleDto[];
    permissions: UserPermissionDto[];
}
