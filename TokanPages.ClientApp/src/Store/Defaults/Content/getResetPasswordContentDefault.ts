import { IContentResetPassword } from "../../States";

export const GetResetPasswordContentDefault: IContentResetPassword = 
{
    isLoading: false,
    content: 
    {
        language: "",
        caption: "",
        button: "",
        labelEmail: ""
    }    
}