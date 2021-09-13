import { IArticleItem } from "../../../Shared/Components/ContentRender/Models/articleItemModel";

export interface IArticle
{
    isLoading: boolean;
    article: IArticleItem;
}
