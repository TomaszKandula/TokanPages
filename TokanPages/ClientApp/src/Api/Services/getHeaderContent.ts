import { getDataFromUrl } from "../request";
import { IHeaderContentDto } from "../../Api/Models";
import { GET_HEADER_CONTENT } from "../../Shared/constants";

export const getHeaderContent = async (): Promise<IHeaderContentDto> =>
{
    return await getDataFromUrl(GET_HEADER_CONTENT);
};
