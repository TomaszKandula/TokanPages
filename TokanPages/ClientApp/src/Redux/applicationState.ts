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

export interface IArticleItem
{
    id: string;
    title: string;
    description: string;
    isPublished: boolean;
    likes: number;
    readCount: number;
    createdAt: string;
    updatedAt: string;
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
        likes:  0,
        readCount: 0,
        createdAt: "",
        updatedAt: "",
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
        likes: ArticleDefaultValues.article.likes,
        readCount: ArticleDefaultValues.article.readCount,
        createdAt: ArticleDefaultValues.article.createdAt,
        updatedAt: ArticleDefaultValues.article.updatedAt,
        text: ArticleDefaultValues.article.text
    },
    listArticles:
    {
        articles:  ArticlesDefaultValues.articles,
        isLoading: ArticlesDefaultValues.isLoading
    }
};
