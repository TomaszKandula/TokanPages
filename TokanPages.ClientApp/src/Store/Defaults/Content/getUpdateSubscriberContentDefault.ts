import { IContentUpdateSubscriber } from "../../States";

export const GetUpdateSubscriberContentDefault: IContentUpdateSubscriber = 
{
    isLoading: false,
    content: 
    {
        language: "",
        caption: "",
        button: "",
        labelEmail: ""
    }    
}
