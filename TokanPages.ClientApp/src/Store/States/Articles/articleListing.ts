import { IArticleItem } from "../../../Shared/Components/RenderContent/Models";

export interface IArticleListing
{
    isLoading: boolean;
    articles: IArticleItem[];
}
