import { IContentUpdatePassword } from "../../States";

export const GetUpdatePasswordContentDefault: IContentUpdatePassword = 
{
    isLoading: false,
    content: 
    {
        language: "",
        caption: "",
        button: "",
        labelNewPassword: "",
        labelVerifyPassword: ""
    }    
}
