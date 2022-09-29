import { IGetResetPasswordContent } from "../../States";

export const GetResetPasswordContentDefault: IGetResetPasswordContent = 
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