export interface UpdateUserResultDto 
{
    isActivated: boolean;
    userAlias?: string;
    firstName?: string;
    lastName?: string;
    emailAddress?: string;
    userAboutText?: string;
    userImageName?: string;
    userVideoName?: string;
}