import { ContentActivateAccountState } from "../../States";

export const ContentActivateAccount: ContentActivateAccountState = {
    isLoading: false,
    content: {
        language: "",
        onVerifying: {
            text1: "",
            text2: "",
            type: "",
            caption: "",
            button: ""
        },
        onProcessing: {
            type: "Unset",
            caption: "",
            text1: "",
            text2: "",
            button: "",
        },
        onSuccess: {
            noBusinessLock: {
                text1: "",
                text2: ""
            },
            businessLock: {
                text1: "",
                text2: ""
            },
            type: "Unset",
            caption: "",
            button: ""
        },
        onError: {
            type: "Unset",
            caption: "",
            text1: "",
            text2: "",
            button: "",
        },
    },
};
