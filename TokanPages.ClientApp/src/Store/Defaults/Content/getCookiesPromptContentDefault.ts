import { IGetCookiesPromptContent } from "../../States/Content/getCookiesPromptContentState";

export const GetCookiesPromptContentDefault: IGetCookiesPromptContent = 
{
    isLoading: false,
    content: 
    {
        language: "",
        caption: "",
        text: "",
        button: "",
        days: 0
    }
}