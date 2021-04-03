import { getDataFromUrl } from "../request";
import { IUnsubscribeContentDto } from "../../Api/Models";
import { GET_UNSUBSCRIBE_CONTENT } from "../../Shared/constants";

export const getUnsubscribeContent = async (): Promise<IUnsubscribeContentDto> =>
{
    return await getDataFromUrl(GET_UNSUBSCRIBE_CONTENT);
};
