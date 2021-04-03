import { getDataFromUrl } from "../request";
import { INotFoundContentDto } from "../../Api/Models";
import { GET_NOTFOUND_CONTENT } from "../../Shared/constants";

export const getNotFoundContent = async (): Promise<INotFoundContentDto> =>
{
    return await getDataFromUrl(GET_NOTFOUND_CONTENT);
};
