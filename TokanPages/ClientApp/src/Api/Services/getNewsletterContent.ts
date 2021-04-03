import { getDataFromUrl } from "../request";
import { INewsletter } from "../../Api/Models";
import { GET_NEWSLETTER_CONTENT } from "../../Shared/constants";

export const getNewsletterContent = async (): Promise<INewsletter> =>
{
    return await getDataFromUrl(GET_NEWSLETTER_CONTENT);
};
