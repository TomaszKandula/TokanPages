import { ArticleSelectionState } from "../../States";

export const ArticleSelection: ArticleSelectionState = {
    isLoading: false,
    article: {
        id: "",
        title: "",
        description: "",
        isPublished: false,
        likeCount: 0,
        userLikes: 0,
        readCount: 0,
        createdAt: "",
        updatedAt: "",
        languageIso: "eng",
        author: {
            userId: "",
            aliasName: "",
            avatarName: "",
            firstName: "",
            lastName: "",
            shortBio: "",
            registered: "",
        },
        text: [],
    },
};
