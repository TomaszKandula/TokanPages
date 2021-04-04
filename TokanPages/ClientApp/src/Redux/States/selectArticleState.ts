import { IArticleItem } from "../../Shared/ContentRender/Models/articleItemModel";

export interface IArticle
{
    isLoading: boolean;
    article: IArticleItem;
    attachedErrorObject: any;
}
