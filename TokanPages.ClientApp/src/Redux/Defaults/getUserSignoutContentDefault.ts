import { IGetUserSignoutContent } from "../States/getUserSignoutContentState";

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