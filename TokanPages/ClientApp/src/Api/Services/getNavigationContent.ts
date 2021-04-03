import { getDataFromUrl } from "../request";
import { INavigation } from "../../Api/Models";
import { GET_NAVIGATION_CONTENT } from "../../Shared/constants";

export const getNavigationContent = async (): Promise<INavigation> =>
{
    return await getDataFromUrl(GET_NAVIGATION_CONTENT);
};
