import { IFeaturesContentDto } from "../../Api/Models";

export interface IGetFeaturesContent extends IFeaturesContentDto
{
    isLoading: boolean;
}