import { getDataFromUrl } from "../request";
import { IHeader } from "../../Api/Models";
import { GET_HEADER_CONTENT } from "../../Shared/constants";

export const getHeaderContent = async (): Promise<IHeader> =>
{
    return await getDataFromUrl(GET_HEADER_CONTENT);
};
