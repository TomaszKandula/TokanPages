import { getDataFromUrl } from "../request";
import { INotFound } from "../../Api/Models";
import { GET_NOTFOUND_CONTENT } from "../../Shared/constants";

export const getNotFoundContent = async (): Promise<INotFound> =>
{
    return await getDataFromUrl(GET_NOTFOUND_CONTENT);
};
