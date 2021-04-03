import { getDataFromUrl } from "../request";
import { ISignupFormContentDto } from "../../Api/Models";
import { GET_SIGNUP_FORM_CONTENT } from "../../Shared/constants";

export const getSignupFormContent = async (): Promise<ISignupFormContentDto> =>
{
    return await getDataFromUrl(GET_SIGNUP_FORM_CONTENT);
};
