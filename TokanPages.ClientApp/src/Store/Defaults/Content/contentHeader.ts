import { ContentHeaderState } from "../../States";

export const ContentHeader: ContentHeaderState = {
    isLoading: false,
    content: {
        language: "",
        photo: "",
        caption: "",
        subtitle: "",
        description: "",
        action: {
            text: "",
            href: "",
        },
        resume: {
            text: "",
            href: "",
        },
    },
};
