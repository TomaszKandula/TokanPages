import { IGetCookiesPromptContent } from "../../States/Content/getCookiesPromptContentState";

export const GetCookiesPromptContentDefault: IGetCookiesPromptContent = 
{
    isLoading: false,
    content: 
    {
        caption: "",
        text: "",
        button: "",
        days: 0
    }
}