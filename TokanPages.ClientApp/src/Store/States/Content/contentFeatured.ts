import { IFeaturedContentDto } from "../../../Api/Models";

export interface IContentFeatured extends IFeaturedContentDto
{
    isLoading: boolean;
}