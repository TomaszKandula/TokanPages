import { IGetUserSignoutContent } from "../../States/Content/getUserSignoutContentState";

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