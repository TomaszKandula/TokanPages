import { IFeaturesContentDto } from "../../../Api/Models";

export interface IContentFeatures extends IFeaturesContentDto
{
    isLoading: boolean;
}