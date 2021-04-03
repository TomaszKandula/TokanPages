import { getDataFromUrl } from "../request";
import { ICookiesPrompt } from "../../Api/Models";
import { GET_COOKIES_PROMPT_CONTENT } from "../../Shared/constants";

export const getCookiesPromptContent = async (): Promise<ICookiesPrompt> =>
{
    return await getDataFromUrl(GET_COOKIES_PROMPT_CONTENT);
};