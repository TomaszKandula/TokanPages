import { IArticle } from "../../Redux/States/articleState";

export const ArticleDefaultValues: IArticle = 
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
