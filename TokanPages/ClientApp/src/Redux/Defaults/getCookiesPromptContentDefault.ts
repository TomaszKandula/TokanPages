import { IGetCookiesPromptContent } from "../States/getCookiesPromptContentState";

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