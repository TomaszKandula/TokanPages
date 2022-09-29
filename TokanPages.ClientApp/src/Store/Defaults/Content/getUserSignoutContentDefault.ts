import { IGetUserSignoutContent } from "../../States";

export const GetUserSignoutContentDefault: IGetUserSignoutContent = 
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