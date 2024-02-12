import { ContentFooterState } from "../../States";

export const ContentFooter: ContentFooterState = {
    isLoading: false,
    content: {
        language: "",
        terms: {
            text: "",
            href: "",
        },
        policy: {
            text: "",
            href: "",
        },
        copyright: "",
        reserved: "",
        icons: [],
    },
};
