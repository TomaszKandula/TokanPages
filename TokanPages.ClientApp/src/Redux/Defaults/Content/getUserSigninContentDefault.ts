import { IGetUserSigninContent } from "../../States/Content/getUserSigninContentState";

export const GetUserSigninContentDefault: IGetUserSigninContent = 
{
    isLoading: false,
    content: 
    {
        caption: "",
        button: "",
        link1: "",
        link2: ""
    }    
}