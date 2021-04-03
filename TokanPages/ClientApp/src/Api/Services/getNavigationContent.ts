import { getDataFromUrl } from "../request";
import { INavigationContentDto } from "../../Api/Models";
import { GET_NAVIGATION_CONTENT } from "../../Shared/constants";

export const getNavigationContent = async (): Promise<INavigationContentDto> =>
{
    return await getDataFromUrl(GET_NAVIGATION_CONTENT);
};
