import { GetArticlesDto } from "../../../Api/Models";

export interface ArticleListingState {
    isLoading: boolean;
    payload: GetArticlesDto;
}
