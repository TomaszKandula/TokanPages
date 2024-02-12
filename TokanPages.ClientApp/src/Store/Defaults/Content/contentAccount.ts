import { ContentAccountState } from "../../States";

export const ContentAccount: ContentAccountState = {
    isLoading: false,
    content: {
        language: "",
        sectionAccessDenied: {
            accessDeniedCaption: "",
            accessDeniedPrompt: "",
            homeButtonText: "",
        },
        sectionAccountInformation: {
            caption: "",
            labelUserId: "",
            labelUserAlias: "",
            labelFirstName: "",
            labelLastName: "",
            labelEmail: "",
            labelShortBio: "",
            labelUserAvatar: "",
            labelIsActivated: "",
            isActivatedText: "",
            updateButtonText: "",
            uploadAvatarButtonText: "",
        },
        sectionAccountPassword: {
            caption: "",
            labelOldPassword: "",
            labelNewPassword: "",
            labelConfirmPassword: "",
            updateButtonText: "",
        },
        sectionAccountRemoval: {
            caption: "",
            warningText: "",
            deleteButtonText: "",
        },
    },
};
