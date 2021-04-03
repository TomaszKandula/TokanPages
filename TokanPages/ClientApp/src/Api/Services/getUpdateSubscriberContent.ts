import { getDataFromUrl } from "../request";
import { IUpdateSubscriberContentDto } from "../../Api/Models";
import { GET_UPDATE_SUBSCRIBER_CONTENT } from "../../Shared/constants";

export const getUpdateSubscriberContent = async (): Promise<IUpdateSubscriberContentDto> =>
{
    return await getDataFromUrl(GET_UPDATE_SUBSCRIBER_CONTENT);
};
