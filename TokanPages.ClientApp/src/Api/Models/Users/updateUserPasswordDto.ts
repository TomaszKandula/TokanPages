export interface IUpdateUserPasswordDto
{
    id: string;
    resetId?: string;
    oldPassword?: string;
    newPassword: string;
}