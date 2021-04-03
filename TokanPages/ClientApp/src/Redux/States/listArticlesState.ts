import { IArticleItem } from "./Common/articleItem";

export interface IArticles
{
    isLoading: boolean;
    articles: IArticleItem[];
}
