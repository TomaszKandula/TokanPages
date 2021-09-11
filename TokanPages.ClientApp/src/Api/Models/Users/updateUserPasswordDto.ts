export interface IUpdateUserPasswordDto
{
    Id: string;
    ResetId?: string;
    NewPassword: string;
}