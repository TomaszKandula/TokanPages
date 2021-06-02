import { ICookiesPromptContentDto } from "../../Api/Models";

export interface IGetCookiesPromptContent extends ICookiesPromptContentDto
{
    isLoading: boolean;
}