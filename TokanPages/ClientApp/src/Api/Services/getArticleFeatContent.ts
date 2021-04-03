import { getDataFromUrl } from "../request";
import { IArticleFeat } from "../../Api/Models";
import { GET_ARTICLE_FEAT_CONTENT } from "../../Shared/constants";

export const getArticleFeatContent = async (): Promise<IArticleFeat> =>
{
    return await getDataFromUrl(GET_ARTICLE_FEAT_CONTENT); 
};
