import { IArticleItem } from "../../../Shared/Components/ContentRender/Models";

export interface IArticles
{
    isLoading: boolean;
    articles: IArticleItem[];
}
