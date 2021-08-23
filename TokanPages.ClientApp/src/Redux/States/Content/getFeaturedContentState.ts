import { IFeaturedContentDto } from "../../../Api/Models";

export interface IGetFeaturedContent extends IFeaturedContentDto
{
    isLoading: boolean;
}