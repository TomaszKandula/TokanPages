export interface UpdateUserPasswordDto
{
    id?: string;
    resetId?: string;
    oldPassword?: string;
    newPassword: string;
}