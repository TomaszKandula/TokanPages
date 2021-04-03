import { getDataFromUrl } from "../request";
import { ISigninFormContentDto } from "../../Api/Models";
import { GET_SIGNIN_FORM_CONTENT } from "../../Shared/constants";

export const getSigninFormContent = async (): Promise<ISigninFormContentDto> =>
{
    return await getDataFromUrl(GET_SIGNIN_FORM_CONTENT);
};
