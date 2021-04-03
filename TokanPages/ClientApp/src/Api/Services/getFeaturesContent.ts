import { getDataFromUrl } from "../request";
import { IFeatures } from "../../Api/Models";
import { GET_FEATURES_CONTENT } from "../../Shared/constants";

export const getFeaturesContent = async (): Promise<IFeatures> =>
{
    return await getDataFromUrl(GET_FEATURES_CONTENT);
};
