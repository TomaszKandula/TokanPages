import { IGetUserSignoutContent } from "../../States/Content/getUserSignoutContentState";

export const GetUserSignoutContentDefault: IGetUserSignoutContent = 
{
    isLoading: false,
    content: 
    {
        caption: "",
        onProcessing: "",
        onFinish: ""
    }    
}