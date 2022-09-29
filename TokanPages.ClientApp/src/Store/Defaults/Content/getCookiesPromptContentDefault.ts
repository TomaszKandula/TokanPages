import { IGetCookiesPromptContent } from "../../States";

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