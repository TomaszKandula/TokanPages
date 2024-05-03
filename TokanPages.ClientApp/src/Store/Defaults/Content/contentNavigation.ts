import { ContentNavigationState } from "../../States";

export const ContentNavigation: ContentNavigationState = {
    isLoading: false,
    content: {
        language: "",
        logo: "",
        userInfo: {
            textUserAlias: "",
            textRegistered: "",
            textRoles: "",
            textPermissions: "",
            textButton: "",
        },
        menu: {
            image: "",
            items: [],
        },
    },
};
