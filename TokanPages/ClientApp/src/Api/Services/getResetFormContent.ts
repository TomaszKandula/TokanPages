import { getDataFromUrl } from "../request";
import { IResetFormContentDto } from "../../Api/Models";
import { GET_RESET_FORM_CONTENT } from "../../Shared/constants";

export const getResetFormContent = async (): Promise<IResetFormContentDto> =>
{
    return await getDataFromUrl(GET_RESET_FORM_CONTENT);
};
