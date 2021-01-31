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
    readCount: number;
    createdAt: string;
    updatedAt: string;
    author: IAuthor;
    text: ITextItem[];
}

export interface AppThunkAction<TAction> 
{
    (dispatch: (action: TAction) => void, getState: () => IApplicationState): void;
}

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
