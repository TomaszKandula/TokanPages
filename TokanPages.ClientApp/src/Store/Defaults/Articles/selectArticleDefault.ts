import { IArticle } from "../../States";

export const SelectArticleDefault: IArticle = 
{
    isLoading: false,
    article:
    {
        id: "",
        title: "",
        description: "",
        isPublished: false,
        likeCount:  0,
        userLikes: 0,
        readCount: 0,
        createdAt: "",
        updatedAt: "",
        author: 
        { 
            aliasName: "", 
            avatarName: "",
            firstName: "",
            lastName: "",
            shortBio: "",
            registered: ""
        },
        text: []
    }
};
