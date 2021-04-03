import { getDataFromUrl } from "../request";
import { IFeaturesContentDto } from "../../Api/Models";
import { GET_FEATURES_CONTENT } from "../../Shared/constants";

export const getFeaturesContent = async (): Promise<IFeaturesContentDto> =>
{
    return await getDataFromUrl(GET_FEATURES_CONTENT);
};
