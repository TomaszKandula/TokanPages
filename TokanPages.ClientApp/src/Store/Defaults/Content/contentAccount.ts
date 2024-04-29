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
            labelEmailStatus: {
                label: "",
                negative: "",
                positive: ""
            },
            labelUserAlias: "",
            labelFirstName: "",
            labelLastName: "",
            labelEmail: "",
            labelShortBio: "",
            labelUserAvatar: "",
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
        sectionAccountDeactivation: {
            caption: "",
            warningText: "",
            deactivateButtonText: ""
        },
        sectionAccountRemoval: {
            caption: "",
            warningText: "",
            deleteButtonText: "",
        },
    },
};
