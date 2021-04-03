import { getDataFromUrl } from "../request";
import { ISignupForm } from "../../Api/Models";
import { GET_SIGNUP_FORM_CONTENT } from "../../Shared/constants";

export const getSignupFormContent = async (): Promise<ISignupForm> =>
{
    return await getDataFromUrl(GET_SIGNUP_FORM_CONTENT);
};
