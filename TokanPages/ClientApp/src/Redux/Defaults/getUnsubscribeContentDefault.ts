import { IGetUnsubscribeContent } from "../States/getUnsubscribeContentState";

export const GetUnsubscribeContentDefault: IGetUnsubscribeContent = 
{
    isLoading: false,
    content: 
    {
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