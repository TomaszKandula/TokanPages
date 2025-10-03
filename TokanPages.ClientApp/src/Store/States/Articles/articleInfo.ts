import { RetrieveArticleInfoResponseDto } from "../../../Api/Models";

export interface ArticleInfoState {
    isLoading: boolean;
    info: RetrieveArticleInfoResponseDto;
}
