export interface IAccountContentDto
{
    content:
    {
        sectionAccessDenied: ISectionAccessDenied,
        sectionBasicInformation: ISectionBasicInformation
    }
}

export interface ISectionAccessDenied
{
    accessDeniedCaption: string;
    accessDeniedPrompt: string;
    homeButtonText: string;
}

export interface ISectionBasicInformation
{
    caption: string;
    labelUserId: string;
    labelUserAlias: string;
    labelFirstName: string;
    labelLastName: string;
    labelEmail: string;
    labelShortBio: string;
    labelUserAvatar: string;
    updateButtonText: string;
    uploadAvatarButtonText: string;
}
