import { IGetUserSigninContent } from "../../States";

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