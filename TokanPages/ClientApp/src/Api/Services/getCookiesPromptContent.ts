import { getDataFromUrl } from "../request";
import { ICookiesPromptContentDto } from "../../Api/Models";
import { GET_COOKIES_PROMPT_CONTENT } from "../../Shared/constants";

export const getCookiesPromptContent = async (): Promise<ICookiesPromptContentDto> =>
{
    return await getDataFromUrl(GET_COOKIES_PROMPT_CONTENT);
};