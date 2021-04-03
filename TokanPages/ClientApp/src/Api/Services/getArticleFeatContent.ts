import { getDataFromUrl } from "../request";
import { IArticleFeatContentDto } from "../../Api/Models";
import { GET_ARTICLE_FEAT_CONTENT } from "../../Shared/constants";

export const getArticleFeatContent = async (): Promise<IArticleFeatContentDto> =>
{
    return await getDataFromUrl(GET_ARTICLE_FEAT_CONTENT); 
};
