import { getDataFromUrl } from "../request";
import { INewsletterContentDto } from "../../Api/Models";
import { GET_NEWSLETTER_CONTENT } from "../../Shared/constants";

export const getNewsletterContent = async (): Promise<INewsletterContentDto> =>
{
    return await getDataFromUrl(GET_NEWSLETTER_CONTENT);
};
