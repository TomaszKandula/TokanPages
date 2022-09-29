import { IGetUpdateSubscriberContent } from "../../States";

export const GetUpdateSubscriberContentDefault: IGetUpdateSubscriberContent = 
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
