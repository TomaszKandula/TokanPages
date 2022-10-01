import { IContentUserSignout } from "../../States";

export const GetUserSignoutContentDefault: IContentUserSignout = 
{
    isLoading: false,
    content: 
    {
        language: "",
        caption: "",
        onProcessing: "",
        onFinish: "",
        buttonText: ""
    }    
}