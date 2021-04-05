import { IArticleItem } from "../../Shared/Components/ContentRender/Models/articleItemModel";

export interface IArticles
{
    isLoading: boolean;
    articles: IArticleItem[];
}
