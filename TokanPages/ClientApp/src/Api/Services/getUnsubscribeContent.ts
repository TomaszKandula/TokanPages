import { getDataFromUrl } from "../request";
import { IUnsubscribe } from "../../Api/Models";
import { GET_UNSUBSCRIBE_CONTENT } from "../../Shared/constants";

export const getUnsubscribeContent = async (): Promise<IUnsubscribe> =>
{
    return await getDataFromUrl(GET_UNSUBSCRIBE_CONTENT);
};
