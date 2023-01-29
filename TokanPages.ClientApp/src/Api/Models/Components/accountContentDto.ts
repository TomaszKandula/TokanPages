export interface AccountContentDto
{
    content:
    {
        language: string;
        sectionAccessDenied: SectionAccessDenied,
        sectionAccountInformation: SectionAccountInformation,
        sectionAccountPassword: SectionAccountPassword,
        sectionAccountRemoval: SectionAccountRemoval
    }
}

export interface SectionAccessDenied
{
    accessDeniedCaption: string;
    accessDeniedPrompt: string;
    homeButtonText: string;
}

export interface SectionAccountInformation
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

export interface SectionAccountPassword
{
    caption: string;
    labelOldPassword: string;
    labelNewPassword: string;
    labelConfirmPassword: string;
    updateButtonText: string;
}

export interface SectionAccountRemoval
{
    caption: string;
    warningText: string;
    deleteButtonText: string;
}
