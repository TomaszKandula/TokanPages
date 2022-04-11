import { IGetUpdateSubscriberContent } from "../../../Redux/States/Content/getUpdateSubscriberContentState";

export const GetUpdateSubscriberContentDefault: IGetUpdateSubscriberContent = 
{
    isLoading: false,
    content: 
    {
        caption: "",
        button: "",
        labelEmail: ""
    }    
}
