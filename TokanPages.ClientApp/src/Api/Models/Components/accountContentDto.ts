export interface AccountContentDto {
    content: {
        language: string;
        sectionAccessDenied: SectionAccessDenied;
        sectionAccountInformation: SectionAccountInformation;
        sectionAccountPassword: SectionAccountPassword;
        sectionAccountDeactivation: SectionAccountDeactivation;
        sectionAccountRemoval: SectionAccountRemoval;
    };
}

export interface SectionAccessDenied {
    accessDeniedCaption: string;
    accessDeniedPrompt: string;
    homeButtonText: string;
}

export interface SectionAccountInformation {
    caption: string;
    labelUserId: string;
    labelEmailStatus: {
        label: string;
        negative: string;
        positive: string;
    };
    labelUserAlias: string;
    labelFirstName: string;
    labelLastName: string;
    labelEmail: string;
    labelShortBio: string;
    labelUserAvatar: string;
    updateButtonText: string;
    uploadAvatarButtonText: string;
}

export interface SectionAccountPassword {
    caption: string;
    labelOldPassword: string;
    labelNewPassword: string;
    labelConfirmPassword: string;
    updateButtonText: string;
}

export interface SectionAccountDeactivation {
    caption: string;
    warningText: string;
    deactivateButtonText: string;
}

export interface SectionAccountRemoval {
    caption: string;
    warningText: string;
    deleteButtonText: string;
}
