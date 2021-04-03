import { getDataFromUrl } from "../request";
import { IFeaturedContentDto } from "../../Api/Models";
import { GET_FEATURED_CONTENT } from "../../Shared/constants";

export const getFeaturedContent = async (): Promise<IFeaturedContentDto> =>
{
    return await getDataFromUrl(GET_FEATURED_CONTENT);
};
