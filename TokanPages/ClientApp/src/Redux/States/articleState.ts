import { ITextItem } from "../../Shared/ContentRender/Model/textModel";

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
