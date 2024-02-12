import { FeaturedContentDto } from "../../../Api/Models";

export interface ContentFeaturedState extends FeaturedContentDto {
    isLoading: boolean;
}
