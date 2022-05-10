export interface IUpdateUserDto
{
    id: string;
    isActivated: boolean;
    userAlias?: string;
    firstName?: string;
    lastName?: string;
    emailAddress?: string;
    shortBio?: string;
}
