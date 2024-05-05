import { ArticleContentDto } from "../../../Api/Models";

export interface ContentArticleState extends ArticleContentDto {
    isLoading: boolean;
}
