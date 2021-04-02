import { ITextItem } from "Shared/ContentRender/Model/textModel";

export interface IApplicationState 
{
    selectArticle: IArticle;
    listArticles: IArticles;
}

export interface IListArticles
{
    listArticles: IArticles;
}

export interface IArticle
{
    isLoading: boolean;
    article: IArticleItem;
}

export interface IArticles
{
    isLoading: boolean;
    articles: IArticleItem[];
}

export interface IAuthor
{
    aliasName: string;
    avatarName: string;
    firstName: string;
    lastName: string;
    shortBio: string;
    registered: string;
}

export interface IArticleItem
{
    id: string;
    title: string;
    description: string;
    isPublished: boolean;
    likeCount: number;
    userLikes: number;
    readCount: number;
    createdAt: string;
    updatedAt: string;
    author: IAuthor;
    text: ITextItem[];
}

export interface ISendMessage
{
    isSendingMessage: boolean;
    hasSentMessage: boolean;
}

export interface IAddSubscriber
{
    isAddingSubscriber: boolean;
    hasAddedSubscriber: boolean;
}

export interface IUpdateSubscriber
{
    isUpdatingSubscriber: boolean;
    hasUpdatedSubscriber: boolean;
}

export interface IRemoveSubscriber
{
    isRemovingSubscriber: boolean;
    hasRemovedSubscriber: boolean;
}

export interface IUpdateArticle
{
    isUpdatingArticle: boolean;
    hasUpdatedArticle: boolean;
}

export interface AppThunkAction<TAction> 
{
    (dispatch: (action: TAction) => void, getState: () => IApplicationState): void;
}

// APPLICATION STATE DEFAULTS

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

export const ArticlesDefaultValues: IArticles = 
{
    isLoading: false,
    articles: []
};

export const DefaultAppState = 
{
    selectArticle:
    {
        id: ArticleDefaultValues.article.id,
        title: ArticleDefaultValues.article.title,
        description: ArticleDefaultValues.article.description,
        isPublished: ArticleDefaultValues.article.isPublished,
        likeCount: ArticleDefaultValues.article.likeCount,
        userLikes: ArticleDefaultValues.article.userLikes,
        readCount: ArticleDefaultValues.article.readCount,
        createdAt: ArticleDefaultValues.article.createdAt,
        updatedAt: ArticleDefaultValues.article.updatedAt,
        author: ArticleDefaultValues.article.author,
        text: ArticleDefaultValues.article.text
    },
    listArticles:
    {
        articles:  ArticlesDefaultValues.articles,
        isLoading: ArticlesDefaultValues.isLoading
    }
};

export const SendMessageDefaultValues: ISendMessage = 
{
    isSendingMessage: false,
    hasSentMessage: false
}

export const AddSubscriberDefaultValues: IAddSubscriber = 
{
    isAddingSubscriber: false,
    hasAddedSubscriber: false
}

export const UpdateSubscriberDefaultValues: IUpdateSubscriber = 
{
    isUpdatingSubscriber: false,
    hasUpdatedSubscriber: false
}

export const RemoveSubscriberDefaultValues: IRemoveSubscriber = 
{
    isRemovingSubscriber: false,
    hasRemovedSubscriber: false
}

export const UpdateArticleDefaultValues: IUpdateArticle = 
{
    isUpdatingArticle: false,
    hasUpdatedArticle: false
}
