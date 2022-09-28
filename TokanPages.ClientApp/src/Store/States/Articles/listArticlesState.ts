import { IArticleItem } from "../../../Shared/Components/RenderContent/Models";

export interface IArticles
{
    isLoading: boolean;
    articles: IArticleItem[];
}
