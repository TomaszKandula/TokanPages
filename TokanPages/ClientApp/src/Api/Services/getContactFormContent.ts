import { getDataFromUrl } from "../request";
import { IContactForm } from "../../Api/Models";
import { GET_CONTACT_FORM_CONTENT } from "../../Shared/constants";

export const getContactFormContent = async (): Promise<IContactForm> =>
{
    return await getDataFromUrl(GET_CONTACT_FORM_CONTENT);
};
