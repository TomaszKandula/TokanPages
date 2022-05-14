export interface IAccountContentDto
{
    content:
    {
        sectionAccessDenied: ISectionAccessDenied,
        sectionAccountInformation: ISectionAccountInformation,
        sectionAccountPassword: ISectionAccountPassword,
        sectionAccountRemoval: ISectionAccountRemoval
    }
}

export interface ISectionAccessDenied
{
    accessDeniedCaption: string;
    accessDeniedPrompt: string;
    homeButtonText: string;
}

export interface ISectionAccountInformation
{
    caption: string;
    labelUserId: string;
    labelUserAlias: string;
    labelFirstName: string;
    labelLastName: string;
    labelEmail: string;
    labelShortBio: string;
    labelUserAvatar: string;
    labelIsActivated: string;
    isActivatedText: string;
    updateButtonText: string;
    uploadAvatarButtonText: string;
}

export interface ISectionAccountPassword
{
    caption: string;
    labelOldPassword: string;
    labelNewPassword: string;
    labelConfirmPassword: string;
    updateButtonText: string;
}

export interface ISectionAccountRemoval
{
    warningText: string;
    deleteButtonText: string;
}
