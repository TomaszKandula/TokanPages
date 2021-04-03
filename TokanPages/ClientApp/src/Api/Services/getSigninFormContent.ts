import { getDataFromUrl } from "../request";
import { ISigninForm } from "../../Api/Models";
import { GET_SIGNIN_FORM_CONTENT } from "../../Shared/constants";

export const getSigninFormContent = async (): Promise<ISigninForm> =>
{
    return await getDataFromUrl(GET_SIGNIN_FORM_CONTENT);
};
