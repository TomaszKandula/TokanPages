import { IContentUnsubscribe } from "../../States";

export const GetUnsubscribeContentDefault: IContentUnsubscribe = 
{
    isLoading: false,
    content: 
    {
        language: "",
        contentPre:
        {
            caption: "",
            text1: "",
            text2: "",
            text3: "",
            button: ""
        },
        contentPost:
        {
            caption: "",
            text1: "",
            text2: "",
            text3: "",
            button: ""
        }
    }    
}