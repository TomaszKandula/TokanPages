import { getDataFromUrl } from "../request";
import { IFeatured } from "../../Api/Models";
import { GET_FEATURED_CONTENT } from "../../Shared/constants";

export const getFeaturedContent = async (): Promise<IFeatured> =>
{
    return await getDataFromUrl(GET_FEATURED_CONTENT);
};
