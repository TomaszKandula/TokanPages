import { IArticleItem } from "../../Shared/ContentRender/Models/articleItemModel";

export interface IArticles
{
    isLoading: boolean;
    articles: IArticleItem[];
    attachedErrorObject: any;
}
