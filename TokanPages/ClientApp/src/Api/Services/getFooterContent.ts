import { getDataFromUrl } from "../request";
import { IFooter } from "../../Api/Models";
import { GET_FOOTER_CONTENT } from "../../Shared/constants";

export const getFooterContent = async (): Promise<IFooter> =>
{
    return await getDataFromUrl(GET_FOOTER_CONTENT);
};
