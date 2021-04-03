import { getDataFromUrl } from "../request";
import { IContactFormContentDto } from "../../Api/Models";
import { GET_CONTACT_FORM_CONTENT } from "../../Shared/constants";

export const getContactFormContent = async (): Promise<IContactFormContentDto> =>
{
    return await getDataFromUrl(GET_CONTACT_FORM_CONTENT);
};
