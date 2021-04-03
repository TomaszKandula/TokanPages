import { getDataFromUrl } from "../request";
import { IResetForm } from "../../Api/Models";
import { GET_RESET_FORM_CONTENT } from "../../Shared/constants";

export const getResetFormContent = async (): Promise<IResetForm> =>
{
    return await getDataFromUrl(GET_RESET_FORM_CONTENT);
};
