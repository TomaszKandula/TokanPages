import { getDataFromUrl } from "../request";
import { IWrongPagePromptContentDto } from "../Models";
import { GET_WRONG_PAGE_PROMPT_CONTENT } from "../../Shared/constants";

export const getWrongPagePromptContent = async (): Promise<IWrongPagePromptContentDto> =>
{
    return await getDataFromUrl(GET_WRONG_PAGE_PROMPT_CONTENT);
};
