import { getDataFromUrl } from "../request";
import { IUpdateSubscriber } from "../../Api/Models";
import { GET_UPDATE_SUBSCRIBER_CONTENT } from "../../Shared/constants";

export const getUpdateSubscriberContent = async (): Promise<IUpdateSubscriber> =>
{
    return await getDataFromUrl(GET_UPDATE_SUBSCRIBER_CONTENT);
};
