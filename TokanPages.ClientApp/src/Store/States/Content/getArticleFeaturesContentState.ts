import { IArticleFeaturesContentDto } from "../../../Api/Models";

export interface IGetArticleFeaturesContent extends IArticleFeaturesContentDto
{ 
    isLoading: boolean;
}