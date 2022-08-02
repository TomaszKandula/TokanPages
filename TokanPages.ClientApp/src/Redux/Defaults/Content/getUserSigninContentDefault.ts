import { IGetUserSigninContent } from "../../States/Content/getUserSigninContentState";

export const GetUserSigninContentDefault: IGetUserSigninContent = 
{
    isLoading: false,
    content: 
    {
        language: "",
        caption: "",
        button: "",
        link1: "",
        link2: "",
        labelEmail: "",
        labelPassword: ""
    }    
}