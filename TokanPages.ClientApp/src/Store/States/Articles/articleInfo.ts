import { ArticleItemBase } from "../../../Shared/Components/RenderContent/Models";

export interface ArticleInfoState {
    isLoading: boolean;
    collectedInfo: ArticleItemBase[] | undefined;
}
