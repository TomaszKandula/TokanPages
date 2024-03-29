export interface UpdateUserDto {
    id?: string;
    isActivated: boolean;
    userAlias?: string;
    firstName?: string;
    lastName?: string;
    emailAddress?: string;
    userAboutText?: string;
    userImageName?: string;
    userVideoName?: string;
}
