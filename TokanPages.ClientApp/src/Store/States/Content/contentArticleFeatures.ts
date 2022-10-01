import { IArticleFeaturesContentDto } from "../../../Api/Models";

export interface IContentArticleFeatures extends IArticleFeaturesContentDto
{ 
    isLoading: boolean;
}