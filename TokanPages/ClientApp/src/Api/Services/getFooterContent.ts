import { getDataFromUrl } from "../request";
import { IFooterContentDto } from "../../Api/Models";
import { GET_FOOTER_CONTENT } from "../../Shared/constants";

export const getFooterContent = async (): Promise<IFooterContentDto> =>
{
    return await getDataFromUrl(GET_FOOTER_CONTENT);
};
