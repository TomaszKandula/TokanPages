import { IContentUserSignin } from "../../States";

export const GetUserSigninContentDefault: IContentUserSignin = 
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