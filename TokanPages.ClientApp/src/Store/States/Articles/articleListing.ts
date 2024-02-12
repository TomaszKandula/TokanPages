import { ArticleItem } from "../../../Shared/Components/RenderContent/Models";

export interface ArticleListingState {
    isLoading: boolean;
    articles: ArticleItem[];
}
